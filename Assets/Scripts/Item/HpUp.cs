using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : Item
{
    public override void GiveEffect()
    {
        if (Refueling.Instance.canUse)
        {
            Refueling.Instance.StartCoroutine("refueling");
        }
    }
}
