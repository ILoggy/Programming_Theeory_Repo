using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public float xBound;
    public float zBound;

    public Rigidbody packageRb;

    public GameObject playerModel;

    private GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        packageRb = GetComponent<Rigidbody>();
        playerModel = GameObject.Find("Player Model");
        GameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch Box"))
        {
            packageRb.AddForce(playerModel.transform.forward * 60, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Kick Box"))
        {
            packageRb.AddForce(playerModel.transform.forward * 90, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Package Goal"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            GameManager.SetPointsValue(100);
        }
    }
}
