using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager audioInstance;
    public static AudioManager AudioInstance { get { return audioInstance; } }

    private void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }
    #endregion


    private AudioSource bgmSource;
    private ObjectPool sfxPool;

    [SerializeField] private List<AudioClip> bgmList = new();
    [SerializeField] private SFXController sfxPrefab;

    private void Init()
    {
        bgmSource = GetComponent<AudioSource>();

        sfxPool = new ObjectPool(transform, 10, sfxPrefab);
    }

    public void BgmPlay(int index)
    {
        if (0 <= index && index < bgmList.Count)
        {
            bgmSource.Stop();
            bgmSource.clip = bgmList[index];
            bgmSource.Play();
        }

    }

    public SFXController GetSFX()
    {
        PooledObject po = sfxPool.GetPool(); 
        return po as SFXController;
    }
}
