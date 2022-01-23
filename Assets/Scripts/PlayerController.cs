using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;

    private Rigidbody playerRb;

    private GameObject focalPoint;
    public GameObject punchBox;
    public GameObject kickBox;

    public GameManager gameManager;

    public bool hasGem;
    public bool hasRadioactive;
    public bool hasPower;
    public bool hasGoldCuplcon;

    private bool canPunch = true;
    private bool canKick = true;
    private bool canDash = true;
    [SerializeField]private bool canJump = true;

    public float jumpForce;
    [SerializeField] private float gravity;

    public bool playKeyboard;

    //PowerUps
    Gem Gem;
    Radioactive Radioactive;
    GoldCuplcon GoldCuplcon;
    Power Power;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focial Point");
        

        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Every frame call this methods
    void Update()
    {
        Physics.gravity = new Vector3(0, gravity, 0);

        if (canJump)
        {
            Movement();
        }

        if (playKeyboard && Input.GetKeyDown(KeyCode.E))
        {
            PowerUpUseKB();
        }
        else if (!playKeyboard && Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            PowerUpUseJS();
        }
            
        PunchUse();
        KickUse();
        Jump();

        if (playKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && canJump)
            {
                StartCoroutine("Dash");
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Joystick1Button2) && canDash && canJump))
            {
                StartCoroutine("Dash");
            }
        }

        if (transform.position.y <= 0.05f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    //player movement at Horizontal and Vertical Axis
    void Movement()
    {
        if (playKeyboard)
        {
            if (Input.GetKey(KeyCode.A))
            {
                playerRb.AddForce(focalPoint.transform.right * -velocity * Time.deltaTime, ForceMode.Force);
            }
                
            if (Input.GetKey(KeyCode.D))
            {
                playerRb.AddForce(focalPoint.transform.right * velocity * Time.deltaTime, ForceMode.Force);
            }
               
            if (Input.GetKey(KeyCode.W))
            {
                playerRb.AddForce(focalPoint.transform.forward * velocity * Time.deltaTime, ForceMode.Force);
            }
                
            if (Input.GetKey(KeyCode.S))
            {
                playerRb.AddForce(focalPoint.transform.forward * -velocity * Time.deltaTime, ForceMode.Force);
            } 
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            playerRb.AddForce(focalPoint.transform.forward * velocity * verticalInput * Time.deltaTime, ForceMode.Force);
            playerRb.AddForce(focalPoint.transform.right * velocity * horizontalInput * Time.deltaTime, ForceMode.Force);
        }
        
    }

    IEnumerator Dash()
    {
        canDash = false;
        playerRb.AddForce(focalPoint.transform.forward * 150, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        canDash = true;
    }

    private void Jump()
    {
        if (playKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                playerRb.AddForce(focalPoint.transform.up * jumpForce, ForceMode.Force);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) && canJump)
            {
                playerRb.AddForce(focalPoint.transform.up * jumpForce, ForceMode.Force);
            }
        }
        
    }

    private void PunchUse()
    {
        if (playKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canPunch == true)
            {
                StartCoroutine("Punch");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button5) && canPunch == true)
            {
                StartCoroutine("Punch");
            }
        }
    }
    
    IEnumerator Punch()
    {
        canPunch = false;
        punchBox.SetActive(true);
        yield return new WaitForSeconds(1);
        punchBox.SetActive(false);
        canPunch = true;
    }
    private void KickUse()
    {
        if (playKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && canKick == true)
            {
                StartCoroutine("Kick");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button4) && canKick == true)
            {
                StartCoroutine("Kick");
            }
        }
        
    }

    IEnumerator Kick()
    {
        canKick = false;
        kickBox.SetActive(true);
        yield return new WaitForSeconds(1);
        kickBox.SetActive(false);
        canKick = true;
    }

    private void PowerUpUseKB()
    {
        GameObject PowerUpManager = FindObjectOfType<PowerUp>().gameObject;
        if (hasGem)
        {
            PowerUpManager.GetComponent<Gem>().Use();
            hasGem = false;
        }

        if (hasGoldCuplcon)
        {
            PowerUpManager.GetComponent<GoldCuplcon>().Use();
            hasGoldCuplcon = false;
        }

        if (hasPower)
        {
            gameManager.UsePower();
            hasPower = false;
        }

        if (hasRadioactive)
        {
            PowerUpManager.GetComponent<Radioactive>().Use();
            hasRadioactive = false;
        }
    }

    private void PowerUpUseJS()
    {
        GameObject PowerUpManager = FindObjectOfType<PowerUp>().gameObject;
        if (hasGem)
        {
            PowerUpManager.GetComponent<Gem>().Use();
            hasGem = false;
        }

        if (hasGoldCuplcon)
        {
            PowerUpManager.GetComponent<GoldCuplcon>().Use();
            hasGoldCuplcon = false;
        }

        if (hasPower)
        {
            gameManager.UsePower();
            hasPower = false;
        }

        if (hasRadioactive)
        {
            PowerUpManager.GetComponent<Radioactive>().Use();
            hasRadioactive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("PowerUp"))
        {
            if (other.gameObject.GetComponent<GoldCuplcon>())
            {
                hasGoldCuplcon = true;
                Destroy(other.gameObject);
                hasGem = false;
                hasPower = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<Gem>())
            {
                hasGem = true;
                Destroy(other.gameObject);
                hasGoldCuplcon = false;
                hasPower = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<Power>())
            {
                hasPower = true;
                Destroy(other.gameObject);
                hasGoldCuplcon = false;
                hasGem = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<Radioactive>())
            {
                hasRadioactive = true;
                Destroy(other.gameObject);
                hasGoldCuplcon = false;
                hasPower = false;
                hasGem = false;
            }
        }
    }
}
