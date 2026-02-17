using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip collectSound; // Assign this in the Inspector
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player using its tag
        if (other.CompareTag("Player"))
        {
            // Assign the sound clip and play it
            // audioSource.clip = collectSound;
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            // Optional: Disable the collider and renderer immediately so it can't be collected again
            // GetComponent<Collider>().enabled = false;
            // Optional: Hide the collectible object
            // GetComponent<Renderer>().enabled = false; 

            // A common issue is destroying the object before the sound finishes.
            // You can destroy the object after a slight delay to allow the sound to play fully.
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
