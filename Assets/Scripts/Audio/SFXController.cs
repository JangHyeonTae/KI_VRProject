using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : PooledObject
{

    AudioSource audioSource;
    private float audioTime;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        audioTime -= Time.deltaTime;

        if(audioTime <= 0)
        {
            ReturnPool();
        }
    }

    public void Play(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

        audioTime = clip.length;
    }
}
