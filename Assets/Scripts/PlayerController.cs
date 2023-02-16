using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    GameObject playerCamera;
    Camera pCamComponent;

    RaycastHit hit;

    public TextMeshProUGUI cursorText;
    public TextMeshProUGUI coinCountText;
    public int coins;

    public float speed;
    float speedConstant;

    bool isGrounded;
    bool didPlayerRaycastHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = transform.GetChild(0).gameObject;
        isGrounded = false;
        speedConstant = speed;
        pCamComponent = playerCamera.GetComponent<Camera>();
        coins = 0;
        
    }


    private void Update()
    {
        cursorText.text = "";

        didPlayerRaycastHit = false;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3, ~6))
            didPlayerRaycastHit = true;

        coinCountText.text = coins.ToString();

        if (Input.GetKeyDown("space"))
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.99f, transform.position.z), Vector3.down, 0.02f))
                isGrounded = true;

        if (Input.GetKey("e") && didPlayerRaycastHit && hit.collider.CompareTag("Cashdoor") && hit.collider.transform.GetComponent<CashDoor>().coinAmount <= coins)
        {
            coins -= hit.collider.transform.GetComponent<CashDoor>().coinAmount;
            GameObject.Destroy(hit.collider.gameObject);
        }

        if (didPlayerRaycastHit && hit.collider.CompareTag("Cashdoor"))
        {
            if (hit.collider.transform.GetComponent<CashDoor>().coinAmount <= coins)
            {
                cursorText.text = "Press (E) to open";
            }
            else
            {
                cursorText.text = "You don't have enough coins yet";
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += speed * 0.6f;
            pCamComponent.fieldOfView = Mathf.Lerp(pCamComponent.fieldOfView, 55, Time.deltaTime * 10);
        }
        else
            pCamComponent.fieldOfView = Mathf.Lerp(pCamComponent.fieldOfView, 60, Time.deltaTime * 10); ;


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
        playerCamera.GetComponent<CameraController>().xRotation = 0;
    }
}
