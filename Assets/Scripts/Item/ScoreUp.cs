using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp : Item
{
    public override void GiveEffect()
    {
        GameManager.Instance.score += 6000;
    }
}
