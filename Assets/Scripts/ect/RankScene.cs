using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankScene : MonoBehaviour
{
    public List<Text> Names = new List<Text>();
    public List<Text> Scores = new List<Text>();
    public List<Text> Stages = new List<Text>();

    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            if(RankManager.Instance.rankScore[i] == 0)
            {
                Names[i].text = "";
                Scores[i].text = "";
                Stages[i].text = "";
            }
            else
            {
                Names[i].text = RankManager.Instance.rankName[i];
                Scores[i].text = RankManager.Instance.rankScore[i].ToString("D8");

                if(RankManager.Instance.rankStage[i] < 4)
                {
                    Stages[i].text = RankManager.Instance.rankStage[i].ToString() + " STAGE";
                }
                else
                {
                    Stages[i].text = "CLEAR";
                }
            }
        }
    }
    public void GoToClearScene()
    {
        SceneManager.LoadScene(5);   
    }
    public void GoToGameClear()
    {
        SceneManager.LoadScene(7);
    }
    public void GoToGameOver()
    {
        SceneManager.LoadScene(8);
    }
}
