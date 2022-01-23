using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radioactive : PowerUp
{
    //POLYMORHISM
    public override void Rotation()
    {
        base.Rotation();
    }

    public override void Use()
    {
        Debug.Log("Radioactive_PowerUp used");
        GameManager.points += 10;

        foreach (GameObject e in enemys)
        {
            Destroy(e);
        }

        foreach (GameObject n in npcs)
        {
            Destroy(n);
        }
    }
}
