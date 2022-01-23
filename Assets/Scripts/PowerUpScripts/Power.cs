using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : PowerUp
{
    //POLYMORHISM
    public override void Rotation()
    {
        base.Rotation();
    }
    public override void Use()
    {
        Debug.Log("Power_PowerUp used");
        GameManager.points += 10;
        StartCoroutine("PowerTimer");
    }

    IEnumerator PowerTimer()
    {
        GameManager.player.velocity *= 2;
        yield return new WaitForSeconds(3);
        GameManager.player.velocity /= 2;
    }
}
