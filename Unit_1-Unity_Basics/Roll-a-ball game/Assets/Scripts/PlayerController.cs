using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
     private Rigidbody rb; 
     private int count;
     public GameObject winTextObject;


     private float movementX;
     private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
       //function body
       Vector2 movementVector = movementValue.Get<Vector2>();
       
       movementX = movementVector.x;
       movementY = movementVector.y;
    }

    void SetCountText()
    {
      countText.text = "Count: " + count.ToString();
      if(count >= 12)
      {
        winTextObject.SetActive(true);
        
        //Destroy Enemy
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
      }

    }

    void FixedUpdate()
    {
      Vector3 movement = new Vector3(movementX, 0.0f, movementY);

      rb.AddForce(movement * speed);
      

      
    }

    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.CompareTag("PickUp"))
      {
        
        other.gameObject.SetActive(false);
        count = count +1;

        SetCountText(); 

      }
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Enemy"))
      {
        // destroy the current Object
        Destroy(gameObject);
        // Set the text to "You Lose"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";
      }

    }
}


