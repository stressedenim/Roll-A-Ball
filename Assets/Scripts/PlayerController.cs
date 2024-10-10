using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;
    private int pickupCount;
    private int totalPickups;
    private float pickupChunk;
    private Timer timer;
    private bool gameOver = false;

    [Header("UI")]
    public GameObject inGamePanel;
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public Image pickupImage;
    public GameObject winPanel;
    public TMP_Text winTimeText; 

    void Start()
    {
        // gets the ridgidbody component attached to this gameObject
        rb = GetComponent<Rigidbody>();
        //gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //assign the total amount of pickups
        totalPickups = pickupCount;
        //set pickup image fill amount to zero
        pickupImage.fillAmount = 0;
        //work out the pickup chunk fill amount
        pickupChunk = 1.0f / totalPickups;
        //run the check pickups function
        CheckPickups();
        //gets the timer object
        timer = FindObjectOfType<Timer>();
        //Starts the timer
        timer.StartTimer();
        //Turn off our win panel
        winPanel.SetActive(false);
        //turn on our ingame panel
        inGamePanel.SetActive(true);
    
    }

    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        //Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //add force to our rigidbody from out movement vector * speed variable
        rb.AddForce(movement * speed * Time.deltaTime);


    }

    private void Update()
    {
        timerText.text = "Time: " + timer.currentTime.ToString("F2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            //destroy the collided object
            Destroy(other.gameObject);
            //decrement the pickup count
            pickupCount--;
            //increase the fill amount of our pickup image
            pickupImage.fillAmount = pickupImage.fillAmount + pickupChunk;
            //run the check pickups function
            CheckPickups();
        }
    }
    private void CheckPickups()
    {
        
        print("Pickups left: " + pickupCount);
        pickupText.text = "Pickups left: " + pickupCount;
        if (pickupCount == 0)
        {
            WinGame();
        }
    }
    private void WinGame()
    {
        //set out game over to be true
        gameOver = true;
        //stop the timer
        timer.StopTimer();

        //display the timer on our win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Turn on our win panel
        winPanel.SetActive(true);

        //turn off in game panel
        inGamePanel.SetActive(false);

        //stop the ball from rolling
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //temporary restart function
    public void resetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
