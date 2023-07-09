using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] bgms;
    public bool isStart;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        try
        {
            if (Boss.Instance.isStartBoss && isStart) 
            {
                isStart = false;
                audioSource.clip = bgms[1];
                audioSource.Play();
            }

        }
        catch
        {
 
            if(audioSource.clip != bgms[1])
            {
                audioSource.clip = bgms[0];
            }

        }
    }
}
