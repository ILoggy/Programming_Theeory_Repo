using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : PowerUp
{
    //POLYMORHISM

    public override void Rotation()
    {
        base.Rotation();
    }

    public override void Use()
    {
        Debug.Log("Gem_PowerUp used");

        foreach (GameObject p in  packages)
        {
            GameManager.SetPointsValue(100);
            Destroy(p);            
        }

        foreach (GameObject pg in packageGoals)
        {
            Destroy(pg);
        }
    }
}
