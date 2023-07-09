using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public int score;
    public float hp;
    public float fuel;
    public int stage;
    public int power;
    public int playtime;
    public string nickname;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DeleteValue()
    {
        nickname = "";
        hp = 0;
        fuel = 0;
        stage = 0;
        power = 1;
        playtime = 0;
    }
    public static DataManager Instance
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
