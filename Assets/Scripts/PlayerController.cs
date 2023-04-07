using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    GameObject pCamera;
    Rigidbody rb;
    Camera pCameraComponent;
    PlayerTrigger groundTrigger;
    enum SoundState {walking, running, sliding, sneaking}
    SoundState soundState;

    float accelerationBaseValue;
    float maxSpeedBaseValue;
    float moveLeftRight;
    float moveForwardBackward;
    float noise;
    float timeStamp;
    float timeStampTwo;
    float jumpTimeStamp;
    float velocityMag;
    float audioTimeDelay;

    bool isGrounded;
    bool upPressed;
    bool downPressed;
    bool rightPressed;
    bool leftPressed;
    bool sprintPressed;
    bool jumpPressed;
    bool interactPressed;
    bool crouchPressed;
    bool disableRegularForce;
    bool overideNextSound;

    double speedXZ;

    RaycastHit hit;

    System.Random RNG = new System.Random();

    public int coins;

    [Header("Properties")]
    public float acceleration;
    public float maxSpeed;
    public float jumpStrength;
    public float sprintMultiplier;
    public float crouchMultiplier;

    [Header("HUD")]
    public Texture2D standingImage;
    public Texture2D crouchingImage;
    public RawImage HUDImage;
    public TextMeshProUGUI cursorText;
    public TextMeshProUGUI coinCountText;

    [Header("Sounds")]
    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;
    public AudioClip step4;
    public AudioClip sliding;



    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;

        rb = GetComponent<Rigidbody>();
        pCamera = transform.GetChild(0).gameObject;
        pCameraComponent = pCamera.GetComponent<Camera>();
        groundTrigger = transform.GetChild(1).GetComponent<PlayerTrigger>();

        accelerationBaseValue = acceleration;
        maxSpeedBaseValue = maxSpeed;

        noise = 0.5f;

        overideNextSound = false;

        upPressed = false;
        downPressed = false;
        rightPressed = false;
        leftPressed = false;
        sprintPressed = false;
        jumpPressed = false;
        interactPressed = false;
        crouchPressed = false;

        timeStampTwo = Time.time;
        jumpTimeStamp = -0.2f;
    }


    private void Update()
    {
        //grabs player input and stores it
        if (Input.GetKey(KeyCode.W))
            upPressed = true;

        if (Input.GetKey(KeyCode.S))
            downPressed = true;

        if (Input.GetKey(KeyCode.D))
            rightPressed = true;

        if (Input.GetKey(KeyCode.A))
            leftPressed = true;

        if (Input.GetKey(KeyCode.LeftShift))
            sprintPressed = true;

        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;

        if (Input.GetKeyDown(KeyCode.E))
            interactPressed = true;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
            crouchPressed = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        cursorText.text = "";
        coinCountText.text = coins.ToString();

        if (jumpTimeStamp + 0.2f < Time.time)
            isGrounded = groundTrigger.isObjectHere;

        velocityMag = rb.velocity.magnitude;

        speedXZ = Math.Sqrt(Math.Pow(rb.velocity.x, 2) + Math.Pow(rb.velocity.z, 2));

        //set noise to default value (walking)
        noise = 0.7f;

        //resets drag/gravity incase anything changed them
        rb.drag = 1;
        rb.angularDrag = 0.05f;
        rb.useGravity = true;

        disableRegularForce = false;
        
        //set acceleration and max speed back to normal after sprint
        acceleration = accelerationBaseValue;
        maxSpeed = maxSpeedBaseValue;

        soundState = SoundState.walking;

        //
        if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit, 3, ~6))
        {
            if (hit.collider.CompareTag("Cashdoor"))
            {
                if (hit.collider.transform.GetComponent<CashDoor>().coinAmount <= coins)
                {
                    cursorText.text = "Press (E) to open";
                    if (interactPressed)
                    {
                        coins -= hit.collider.transform.GetComponent<CashDoor>().coinAmount;
                        Destroy(hit.collider.gameObject);
                    }
                } 
                else
                    cursorText.text = "You don't have enough coins yet";
            }
        }

        interactPressed = false;

        //If sprint is input, up both accelertion and speed as well as change fov
        if (sprintPressed && !crouchPressed && (upPressed || leftPressed || rightPressed))
        {
            sprintPressed = false;
            noise = 1f;
            soundState = SoundState.running;
            maxSpeed = maxSpeed * sprintMultiplier;
            acceleration = acceleration * sprintMultiplier;
        }

        //If crouch is input, lower the camera and Shorten the Collider of the player
        if (isGrounded && crouchPressed && !jumpPressed)
        {
            crouchPressed = false;

            //reduces player and camera height
            HUDImage.texture = crouchingImage;
            pCamera.transform.position = new Vector3(pCamera.transform.position.x, Mathf.Lerp(pCamera.transform.position.y, transform.position.y + 0.2f, Time.deltaTime * 10), pCamera.transform.position.z);
            transform.GetComponent<CapsuleCollider>().height = 1;
            transform.GetComponent<CapsuleCollider>().center = new Vector3(0, -0.5f, 0);

            if (speedXZ > 4 && upPressed)
            {
                if (timeStamp == 0)
                    timeStamp = Time.time;

                noise = 0.4f;
                soundState = SoundState.sliding;

                //disable any deceleration
                disableRegularForce = true;

                if (timeStamp + 0.7 > Time.time)
                {
                    rb.drag = 0;
                    rb.angularDrag = 0;
                    rb.useGravity = false;
                }

                maxSpeed = maxSpeed * sprintMultiplier * 1.2f;

                if (speedXZ < maxSpeed)
                {
                    if (timeStamp + 0.7 > Time.time)
                        rb.AddRelativeForce(Vector3.forward);

                    rb.velocity = Vector3.zero;
                    rb.velocity = transform.forward * velocityMag;
                }
            }
            else
            {
                noise = 0.2f;
                soundState = SoundState.sneaking;
                maxSpeed = maxSpeed * crouchMultiplier;
                acceleration = acceleration * crouchMultiplier;
            }

        }
        else
        {
            timeStamp = 0;
            HUDImage.texture = standingImage;
            pCamera.transform.position = new Vector3(pCamera.transform.position.x, Mathf.Lerp(pCamera.transform.position.y, transform.position.y + 0.6f, Time.deltaTime * 10), pCamera.transform.position.z);
            transform.GetComponent<CapsuleCollider>().height = 2;
            transform.GetComponent<CapsuleCollider>().center = Vector3.zero;
        }

        //Bug Fix
        if (sprintPressed && crouchPressed)
        {
            sprintPressed = false;
            crouchPressed = false;
        }

        //Debug.Log(soundState);
        SoundManager();

        //Resets force Adding Variables to zero for the next add force
        moveForwardBackward = 0;
        moveLeftRight = 0;

        //turns WASD Input into the Add Force Variables 
        if (upPressed)
        {
            moveForwardBackward += 1;
            upPressed = false;
        }
        if (downPressed)
        {
            moveForwardBackward -= 1;
            downPressed = false;
        }
        if (rightPressed)
        {
            moveLeftRight += 1;
            rightPressed = false;
        }
        if (leftPressed)
        {
            moveLeftRight -= 1;
            leftPressed = false;
        }

        if (!disableRegularForce)
        {
            
            //Uses the Add force Variables to Add force as well as capping the speed
            if (speedXZ < maxSpeed)
                rb.AddRelativeForce(new Vector3(moveLeftRight, 0, moveForwardBackward).normalized * Time.deltaTime * 1000 * acceleration);

            //Increases deceleration to prevent sliding
            rb.velocity = new Vector3 (rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
        }


        //if the player is on the ground and pressed jump then add force to the y for a jump
        if (jumpPressed && isGrounded)
        {
            rb.AddForce(new Vector3(0.0f, jumpStrength, 0.0f), ForceMode.Impulse);
            jumpTimeStamp = Time.time;
        }

        jumpPressed = false;

        //if standing still make "no noise"
        if (speedXZ < 0.1f || !isGrounded)
            noise = 0.05f;

        //changes fov based on speed
        pCameraComponent.fieldOfView = Mathf.Lerp(pCameraComponent.fieldOfView, 60 + (15 * Mathf.Clamp01(((float)speedXZ - 2) / ((maxSpeedBaseValue * sprintMultiplier * 1.2f) - 2))), Time.deltaTime * 2);
    

    }

    //Takes a Y rotation and sets the player's camera to that direction
    public void RecenterCamera(float yRotation = 0)
    {
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        pCamera.GetComponent<CameraController>().xRotation = 0;
    }

    //gives player postition but helps account for crouching
    public Vector3 GetPosition()
    {
        if (isGrounded && crouchPressed && !sprintPressed)
            return (transform.position + new Vector3(0, -0.5f, 0));
        else
            return (transform.position + new Vector3(0, 0.5f, 0));
    }

    //just gives noise value
    public float GetNoise()
    {
        return noise;
    }

    public void SoundManager()
    {
        audioTimeDelay = 0;

        switch (soundState)
        {
            case SoundState.walking:

                if ((upPressed || downPressed || rightPressed || leftPressed) && isGrounded)
                {
                    audioTimeDelay = 0.4f;
                    GetComponent<AudioSource>().volume = 0.42f;
                    PlaySound(FootSteps());
                }

                break;

            case SoundState.running:

                if ((upPressed || downPressed || rightPressed || leftPressed) && isGrounded)
                {
                    audioTimeDelay = 0.2f;
                    GetComponent<AudioSource>().volume = 0.5f;
                    PlaySound(FootSteps());
                }

                break;

            case SoundState.sneaking:

                if ((upPressed || downPressed || rightPressed || leftPressed) && isGrounded)
                {
                    audioTimeDelay = 0.7f;
                    GetComponent<AudioSource>().volume = 0.26f;
                    PlaySound(FootSteps());
                }

                break;

            case SoundState.sliding:

                GetComponent<AudioSource>().volume = 0.42f;
                if (GetComponent<AudioSource>().clip != sliding)
                    PlaySound(sliding, true, true);
                break;
        }


        void PlaySound(AudioClip audioToPlay, bool overideCurrentSound = false, bool overideNextSound = false)
        {
            if (overideNextSound)
                this.overideNextSound = true;

            if (overideCurrentSound)
            {
                GetComponent<AudioSource>().clip = audioToPlay;
                GetComponent<AudioSource>().Play();
            }
            else
            {
                if ((!GetComponent<AudioSource>().isPlaying && timeStampTwo + audioTimeDelay < Time.time) || this.overideNextSound)
                {
                    timeStampTwo = Time.time;
                    this.overideNextSound = false;
                    GetComponent<AudioSource>().clip = audioToPlay;
                    GetComponent<AudioSource>().Play();
                }
            }
        }


        AudioClip FootSteps()
        {
            AudioClip stepSoundToPlay = step1;

            switch (RNG.Next(1, 5))
            {
                case 1:
                    stepSoundToPlay = step1;
                    break;

                case 2:
                    stepSoundToPlay = step2;
                    break;

                case 3:
                    stepSoundToPlay = step3;
                    break;

                case 4:
                    stepSoundToPlay = step4;
                    break;
            }

            if (stepSoundToPlay == GetComponent<AudioSource>().clip)
            {
                stepSoundToPlay = step1;
                if (stepSoundToPlay == GetComponent<AudioSource>().clip)
                    stepSoundToPlay = step2;
            }

            return stepSoundToPlay;
        }
    }
}
