// i had like 15 CS0246 error messages and including the thing bellkw about the text it fixed 14 of them now i have to fix the ie numerator
using UnityEngine;
using TMPro;  // For TMP_Text
using UnityEngine.UI; // For UI elements like Image
//fir fixing the IEnumerator CS0246 error message
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;
    private int pickupCount;
    private int totalPickups;
    private float pickupChunk;
    private Timer timer;
    private bool gameOver = false;
    //Adding a reset point
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;

    [Header("UI")]
    public GameObject inGamePanel;
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public Image pickupImage;
    public GameObject winPanel;
    public TMP_Text winTimeText;
    // adding the two new ui things i added cuyz i think thats what mading the null error coide
    //removing the souls thing cuz i was confused
    public TMP_Text Addition;

    void Start()
    {
        // gets the ridgidbody component attached to this gameObject
        rb = GetComponent<Rigidbody>();
        //gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //adding this to hopefully help the error with the pickups
        pickupText.text = "Pickups Left: " + pickupCount.ToString();
        //assign the total amount of pickups - now souls - went back to pick ups
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
        //Adding a reset point
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
        

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

        //Adding a reset point
        if (resetting)
            return;

    }

    private void Update() => timerText.text = "Time: " + timer.currentTime.ToString("F2");

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
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
        //trying to show the damn win panel only when the game is won 
        Debug.Log("Win Game called!");  // Debug log to check if this is triggered

    }

    // this void update is about respawning
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    //temporary restart function
    public void resetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 StartPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(StartPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
    }
}
