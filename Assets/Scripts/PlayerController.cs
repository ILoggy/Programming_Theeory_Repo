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
        PowerUpUse();
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
        //Debug.Log("Punch used");
        yield return new WaitForSeconds(1);
        //Debug.Log("Punch can be used");
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
        //Debug.Log("Kick used");
        yield return new WaitForSeconds(1);
        //Debug.Log("Kick can be used");
        kickBox.SetActive(false);
        canKick = true;
    }

    private void PowerUpUse()
    {
        if (playKeyboard)
        {
            if (hasGem && Input.GetKeyDown(KeyCode.E))
            {
                Gem.Use();
                hasGem = false;
            }

            if (hasGoldCuplcon && Input.GetKeyDown(KeyCode.E))
            {
                gameManager.UseGoldCuplcon();
                hasGoldCuplcon = false;
            }

            if (hasPower && Input.GetKeyDown(KeyCode.E))
            {
                gameManager.UsePower();
                hasPower = false;
            }

            if (hasRadioactive && Input.GetKeyDown(KeyCode.E))
            {
                gameManager.UseRadioactive();
                hasRadioactive = false;
            }
        }
        else
        {
            if (hasGem && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                Gem.Use();
                hasGem = false;
            }

            if (hasGoldCuplcon && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                gameManager.UseGoldCuplcon();
                hasGoldCuplcon = false;
            }

            if (hasPower && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                gameManager.UsePower();
                hasPower = false;
            }

            if (hasRadioactive && Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                gameManager.UseRadioactive();
                hasRadioactive = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("PowerUp"))
        {
            if (other.gameObject.GetComponent<PowerUp>().isGoldCuplcon)
            {
                hasGoldCuplcon = true;
                Destroy(other.gameObject);
                hasGem = false;
                hasPower = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<PowerUp>().isGem)
            {
                hasGem = true;
                Destroy(other.gameObject);
                hasGoldCuplcon = false;
                hasPower = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<PowerUp>().isPower)
            {
                hasPower = true;
                Destroy(other.gameObject);
                hasGoldCuplcon = false;
                hasGem = false;
                hasRadioactive = false;
            }

            if (other.gameObject.GetComponent<PowerUp>().isRadioactive)
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
