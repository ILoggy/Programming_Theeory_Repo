using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCuplcon : PowerUp
{
    public override void Rotation()
    {
        base.Rotation();
    }
    public override void Use()
    {
        Debug.Log("GoldCuplcon_PowerUp used");
        GameManager.points += 1500;
    }
}
