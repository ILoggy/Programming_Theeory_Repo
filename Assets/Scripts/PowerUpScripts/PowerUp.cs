using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public bool isGem;
    //public bool isRadioactive;
    //public bool isPower;
    //public bool isGoldCuplcon;

    private float rotSpeed = 180.0f;

    public GameManager GameManager;
    public GameObject[] packages;
    public GameObject[] packageGoals;
    public GameObject[] enemys;
    public GameObject[] npcs;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        packages = GameManager.packages;
        packageGoals = GameManager.packageGoals;
        enemys = GameManager.enemys;
        npcs = GameManager.npcs;
    }

    public virtual void Use(){ }
    public virtual void Rotation()
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }
}
