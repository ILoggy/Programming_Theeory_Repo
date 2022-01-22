using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform[] goals;

    private Rigidbody npcRb;

    private GameManager gameManager;
    public GameObject playerModel;

    private float speed = 15.0f;
    public float minimumDistance;

    public int npcGoalsLength;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerModel = GameObject.Find("Player Model");

        npcRb = GetComponent<Rigidbody>();
        npcGoalsLength = gameManager.npcGoals.Length;
        goals = new Transform[npcGoalsLength];

        for (int i = 0; i < npcGoalsLength; i++)
        {
            goals[i] = gameManager.npcGoals[i].transform;
        }
    }

    public Transform GetClosestGoal()
    {
        Transform closestGoal = null;
        float minimumDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform goal in goals)
        {
            float distance = Vector3.Distance(goal.position, currentPos);
            if (distance < minimumDistance)
            {
                closestGoal = goal;
                minimumDistance = distance;
            }
        }
        return closestGoal;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GetClosestGoal().transform.position) > minimumDistance)
        {
            float speedDeltaTime = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, GetClosestGoal().transform.position, speedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC Goal"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Punch Box"))
        {
            npcRb.AddForce(playerModel.transform.forward * 200, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Kick Box"))
        {
            npcRb.AddForce(playerModel.transform.forward * 300, ForceMode.Impulse);
        }
    }
}
