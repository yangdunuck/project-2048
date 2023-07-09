using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelUp : Item
{
    public override void GiveEffect()
    {
        Player.Instance.curFuel = Player.Instance.maxFuel;
    }
}
