using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{   public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI winText;
    public float jumpForce = 5f;
    public float restartDelay = 2f;
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        // function body
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
   
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            // Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

   void FixedUpdate()
   {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*speed);
        
   }

   // This function is called by the PlayerInput component when the Jump action is performed
    public void OnJump()
    {
        // Add an upward force to the Rigidbody to make the player jump
        // Use ForceMode.Impulse for an instant, sudden force like a jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            // Destroy the current object
            Destroy(gameObject); 
            Invoke("RestartGame", restartDelay);

            RestartGame();
        }
    }

    
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time before restarting
        // Load the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

   void OnTriggerEnter(Collider other)
   {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        
   }
}
