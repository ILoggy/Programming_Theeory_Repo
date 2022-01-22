using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isGem;
    public bool isRadioactive;
    public bool isPower;
    public bool isGoldCuplcon;

    private float rotSpeed = 180.0f;

    GameManager GameManager;
    public GameObject[] packages;
    public GameObject[] packageGoals;
    public float points;
    // Start is called before the first frame update
    void Start()
    {
        packages = GameManager.packages;
        packageGoals = GameManager.packageGoals;
        points = GameManager.points;
    }

    public virtual void Use(){ }
    public virtual void Rotation()
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }
}
