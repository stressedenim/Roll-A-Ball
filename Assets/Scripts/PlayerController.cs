using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;
    private int pickupCount;
    private Timer timer;

    void Start()
    {
    // gets the ridgidbody component attached to this gameObject
    rb = GetComponent<Rigidbody>();
        //gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //run the check pickups function
        CheckPickups();
        //gets the timer object
        timer = FindObjectOfType<Timer>();
        //Starts the timer
        timer.StartTimer();
    }


    void FixedUpdate()
    {
        //Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //add force to our rigidbody from out movement vector * speed variable
        rb.AddForce(movement * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            //destroy the collided object
            Destroy(other.gameObject);
            //decrement the pickup count
            pickupCount--;
            //run the check pickups function
            CheckPickups();
        }
    }
    private void CheckPickups()
    {
        print("Pickups left: " + pickupCount);
        if (pickupCount == 0)
        {
            WinGame();
        }
    }
    private void WinGame()
    {
        timer.StopTimer();
        print("Yay! You Win. Your time was: " + timer.GetTime().ToString("F2"));
    }
}
