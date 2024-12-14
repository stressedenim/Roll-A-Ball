using UnityEngine;

public class TiltControls : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(moveVertical * speed, 0, -moveHorizontal * speed) * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(moveVector);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}