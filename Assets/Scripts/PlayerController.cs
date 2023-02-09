using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;

    public float speed;
    float speedConstant;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        speedConstant = speed;
    }


    private void Update()
    {

        if (Input.GetKeyDown("space"))
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.99f, transform.position.z), Vector3.down, 0.02f))
                isGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += speed * 0.6f;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 50, Time.deltaTime * 10);
        }
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 10); ;


        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime), Space.Self);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0), Space.Self);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);
        }

        speed = speedConstant;

        if (isGrounded)
        {
            rb.AddForce(new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
               isGrounded = false;
        }

    }
}
