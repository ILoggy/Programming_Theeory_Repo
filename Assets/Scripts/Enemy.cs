using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;

    public Transform player;
    public GameObject playerModel;

    public float speed = 20.0f;
    public float miniumDistance;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = Transform.FindObjectOfType<PlayerController>().transform;
        playerModel = GameObject.Find("Player Model");
        StartCoroutine("TimeToDeath");
    }

    IEnumerator TimeToDeath()
    {
        yield return new WaitForSeconds(120);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
         
        Chase();
    }

    void Chase()
    {
        if (Vector3.Distance(transform.position, player.position) > miniumDistance)
        {
            float speedDeltaTime = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Stop");
        }
    }

    IEnumerator Stop()
    {
        speed = 0.0f;
        yield return new WaitForSeconds(2);
        speed = 15.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch Box"))
        {
            enemyRb.AddForce(playerModel.transform.forward * 200 , ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Kick Box"))
        {
            enemyRb.AddForce(playerModel.transform.forward * 300, ForceMode.Impulse);
        }
    }
}
