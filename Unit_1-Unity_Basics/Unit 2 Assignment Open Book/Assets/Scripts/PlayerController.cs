using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //RigidBody of the Player
    private Rigidbody rb;
    //Movement along X and Y axes.
    private float movementX;
    private float movementY;
    // Speed at which the player moves.
    public float speed = 0;
     // Variable to keep track of collected "PickUp" objects.
    private int count;
     // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Enemy"))
      {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
      }
    }


    void Start()
    {
        count = 0;
        rb = GetComponent <Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue)
    {
        // Convert the input Value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
        // Store the X and Y components of the movement. 
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

     void SetCountText()
     {
    

        if (count >=12)
        {
           winTextObject.SetActive(true);
           Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }   
        countText.text = "Count: " + count.ToString();

     }

    // FixedUpdate is called once per fixed frame-rate frame. 
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        // Apply force to the Rigidbody to move the player. 
        rb.AddForce(movement * speed);
    }
    

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag. 
       if (other.gameObject.CompareTag("PickUp"))
       { 
          // Deactivate the collided object (making it disappear).
          other.gameObject.SetActive(false);
          count = count + 1;
          SetCountText();
       }
        
        
    }
}
