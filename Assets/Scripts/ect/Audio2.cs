using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio2 : MonoBehaviour
{
    AudioSource audioSource;
    private static Audio2 instance;
    public bool isStart;
    public AudioClip clip;

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
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        try
        {
            Debug.Log(Player.Instance.gameObject.name);
            audioSource.clip = null;
            isStart = false;
        }
        catch
        {
            if (!isStart)
            {
                isStart = true;
                audioSource.clip = clip;
                audioSource.Play();
            }

        }
    }
    public static Audio2 Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
