using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpSpeed = 500
    private Rigidbody rb;

    void Start()
    {
    // gets the ridgidbody component attached to this gameObject
    rb = GetComponent<Rigidbody>();
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

        if (Input.GetButton("jump"));
        {
            rb.AddForce(new Vector3 (0, jumpSpeed, 0));
        }
    }
}
