using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public GameObject destroyBullet;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            destroyBullet.GetComponent<PlayerBullet>().dmg = 3000;
            destroyBullet.SetActive(true);
            Invoke("DeleteDestoryBullet", 1);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Player.Instance.power = 4;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Player.Instance.curHealCooltime = Player.Instance.healCooltime;
            Player.Instance.isBoomCooltime = false;
            Refueling.Instance.canGiveHp = 5;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Player.Instance.curHp = Player.Instance.maxHp;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Player.Instance.curFuel = Player.Instance.maxFuel;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if(GameManager.Instance.stage < 3)
            {
                GameManager.Instance.StageClear();
            }
            else
            {
                GameManager.Instance.GameClear();
            }
        }
    }
    void DeleteDestoryBullet()
    {
        destroyBullet.SetActive(false);
    }
}
