using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    private static RankManager instance;
    public int[] rankScore = new int[5];
    public int[] rankStage = new int[5];
    public string[] rankName = new string[5];
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        for(int i = 0; i < 5; i++) 
        {
            rankScore[i] = PlayerPrefs.GetInt("Score" + i);
            rankStage[i] = PlayerPrefs.GetInt("Stage" + i);
            rankName[i] = PlayerPrefs.GetString("Name" + i);
        }
    }
    public void RankSet(int score, int stage, string name)
    {
        for(int i = 0; i < 5; i++)
        {
            int tempScore = 0;
            int tempStage = 0;
            string tempName = "";

            rankScore[i] = PlayerPrefs.GetInt("Score" + i);
            rankStage[i] = PlayerPrefs.GetInt("Stage" + i);
            rankName[i] = PlayerPrefs.GetString("Name" + i);
            if(rankScore[i] < score)
            {
                tempScore = score;
                tempStage = stage;
                tempName = name;

                score = rankScore[i];
                stage = rankStage[i];
                name = rankName[i];

                rankScore[i] = tempScore;
                rankStage[i] = tempStage;
                rankName[i] = tempName;
            }
            PlayerPrefs.SetInt("Score" + i, rankScore[i]);
            PlayerPrefs.SetInt("Stage" + i, rankStage[i]);
            PlayerPrefs.SetString("Name" + i, rankName[i]);
        }
    }
    public static RankManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
