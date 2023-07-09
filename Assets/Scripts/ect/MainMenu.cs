using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField nicknameInput;
    public Toggle fullScreenTogle;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void StartBtn()
    {
        if(nicknameInput.text == "") { return; }
        DataManager.Instance.nickname = nicknameInput.text;
        SceneManager.LoadScene(1);
    }
    public void StoryBtn()
    {
        SceneManager.LoadScene(2);
    }
    public void RankBtn()
    {
        SceneManager.LoadScene(3);
    }
    public void ControlBtn()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
