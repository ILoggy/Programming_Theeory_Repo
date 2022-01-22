using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    private float speed = 200.0f;
    public GameObject playerModel;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.Find("PenguinModel");
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (playerController.playKeyboard)
        {
            float horizontalInput = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up, speed * horizontalInput * Time.deltaTime);

            playerModel.transform.rotation = transform.rotation;
        }
        else
        {
            float horizontalInput = Input.GetAxis("HorizontalRight");

            transform.Rotate(Vector3.up, speed * horizontalInput * Time.deltaTime);

            playerModel.transform.rotation = transform.rotation;
        }
        
    }
}
