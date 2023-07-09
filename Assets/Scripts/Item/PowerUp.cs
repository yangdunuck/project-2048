using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    public override void GiveEffect()
    {
        if (Player.Instance.power >= 4)
        {
            GameManager.Instance.score += 10000;
        }
        else
        {
            Player.Instance.power++;
        }
    }
}
