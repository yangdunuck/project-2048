using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBtn : MonoBehaviour
{
    public void gotoMain()
    {
        SceneManager.LoadScene(0);
        Debug.Log("버튼눌림");
    }
}
