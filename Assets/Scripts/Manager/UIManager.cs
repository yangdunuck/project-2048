using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider hpBar;
    public Slider fuelBar;
    public Text score;
    public Text playtime;
    public Slider coolTime;
    public Text Stage;

    public Slider boomCooltime;
    public Slider canGiveHpbar;
    public Slider canGiveHpbar2;
    private void Start()
    {
        Stage.text = GameManager.Instance.stage.ToString() + " STAGE";
    }
    private void Update()
    {
        hpBar.value = Player.Instance.curHp / Player.Instance.maxHp;
        fuelBar.value = Player.Instance.curFuel / Player.Instance.maxFuel;
        score.text = GameManager.Instance.score.ToString("D8");
        playtime.text = (GameManager.Instance.playtime / 60).ToString("D2") + ":" + (GameManager.Instance.playtime % 60).ToString("D2");
        try
        {
            if (!Boss.Instance.isStartBoss)
            {
                coolTime.value = 1;
            }
            else
            {
                
            }
        }
        catch
        {
            if (Refueling.Instance.canGiveHp <= 0)
            {
                coolTime.value = 1;
            }
            else
            {
                coolTime.value = 1 - (float)Player.Instance.curHealCooltime / (float)Player.Instance.healCooltime;
            }
        }
        if(Player.Instance.power <= 1 || !Player.Instance.canAttack || Player.Instance.isBoomCooltime)
        {
            boomCooltime.value = 1;
        }
        else
        {
            boomCooltime.value = 0;
        }
        canGiveHpbar.value = Refueling.Instance.canGiveHp / 5;
        canGiveHpbar2.value = Refueling.Instance.canGiveHp / 5;
        canGiveHpbar.transform.position = Camera.main.WorldToScreenPoint(Refueling.Instance.transform.position + Vector3.down);
    }
    public void GoMenu()
    {
        for(int i = 0; i < GameManager.Instance.Enemys.Count; i++)
        {
            GameManager.Instance.Enemys[i].GetComponent<Enemy>().useEffect = false;
        }
        RankManager.Instance.RankSet(GameManager.Instance.score, GameManager.Instance.stage, DataManager.Instance.nickname);
        DataManager.Instance.DeleteValue();
        SceneManager.LoadScene(0);
    }
}

