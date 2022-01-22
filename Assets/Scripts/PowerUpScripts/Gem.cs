using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : PowerUp
{
    public override void Rotation()
    {
        base.Rotation();
    }

    public override void Use()
    {
        Debug.Log("Gem_PowerUp used");

        foreach (GameObject p in  packages)
        {
            Destroy(p);
            points += 100;
        }

        foreach (GameObject pg in packageGoals)
        {
            Destroy(pg);
        }
    }
}
