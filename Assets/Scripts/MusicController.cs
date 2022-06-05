using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance = null;
    private AudioSource musicSource;

    public static MusicController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        musicSource = this.GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (musicSource.isPlaying)
            return;

        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

}
