using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : Item
{
    public override void GiveEffect()
    {
        Player.Instance.invincibilityTime = 2;
    }
}
