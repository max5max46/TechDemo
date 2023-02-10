using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    GameObject cam;
    Camera camCam;

    public TextMeshProUGUI coinCountText;

    public float speed;
    float speedConstant;

    public int coins;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0).gameObject;
        isGrounded = false;
        speedConstant = speed;
        camCam = cam.GetComponent<Camera>();
        coins = 0;
        
    }


    private void Update()
    {
        coinCountText.text = coins.ToString();

        if (Input.GetKeyDown("space"))
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.99f, transform.position.z), Vector3.down, 0.02f))
                isGrounded = true;

        if (Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, new Vector3(cam.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), out hit, 3))
            {
                if (hit.collider.CompareTag("Cashdoor") && hit.collider.transform.GetComponent<CashDoor>().coinAmount <= coins)
                {
                    coins -= hit.collider.transform.GetComponent<CashDoor>().coinAmount;
                    GameObject.Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += speed * 0.6f;
            camCam.fieldOfView = Mathf.Lerp(camCam.fieldOfView, 55, Time.deltaTime * 10);
        }
        else
            camCam.fieldOfView = Mathf.Lerp(camCam.fieldOfView, 60, Time.deltaTime * 10); ;


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
            rb.AddForce(new Vector3(0.0f, 10.0f, 0.0f), ForceMode.Impulse);
               isGrounded = false;
        }

    }

    public void RecenterCamera(float yRotation = 0)
    {
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        cam.GetComponent<CameraController>().xRotation = 0;
    }
}
