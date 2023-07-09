using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour
{
    public Text score;
    public Text stage;
    public Text hp;
    public Text fuel;
    public Text playtime;
    public Text power;

    private void Start()
    {
        Audio2.Instance.isStart = true;
        score.text = "Score : " + DataManager.Instance.score.ToString("D8");
        try
        {
            stage.text = "Stage : " + (DataManager.Instance.stage - 1).ToString();
        }
        catch { }
        if(DataManager.Instance.hp > 0)
        {
            hp.text = "Hp : " + (DataManager.Instance.hp / 5 * 100).ToString() + "%";
        }
        else
        {
            hp.text = "Hp : 0%";
        }
        if(DataManager.Instance.fuel > 0)
        {
            fuel.text = "Fuel : " + DataManager.Instance.fuel.ToString("F2") + "%";
        }
        else
        {
            fuel.text = "Fuel : 00.00%";
        }
        
        playtime.text = "Playtime : " + (DataManager.Instance.playtime / 60).ToString("D2") + ":" + (DataManager.Instance.playtime % 60).ToString("D2");
        power.text = "Power : " + DataManager.Instance.power.ToString();

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { NextStage(); }
    }
    public void NextStage()
    {
        SceneManager.LoadScene(1);
    }
    public void GoMenu()
    {
        DataManager.Instance.DeleteValue();
        Audio2.Instance.isStart = false;
        SceneManager.LoadScene(0);
    }
    public void GoRank()
    {
        SceneManager.LoadScene(6);
    }
    public void GoRank2()
    {
        SceneManager.LoadScene(9);
    }
    public void GoRank3()
    {
        SceneManager.LoadScene(10);
    }
}
