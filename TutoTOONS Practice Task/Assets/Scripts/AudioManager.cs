using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource music, sound;
    [SerializeField]
    AudioClip mainMenuMusic, levelMusic, levelWonMusic, UIclick, correctDotClick, wrongDotClick;

    public static AudioManager Instance;

    void Awake()
    {
        Instance = this;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMainTheme();
        }
        else
        {
            PlayLevelTheme();
        }
    }

    public void PlayMainTheme()
    {
        music.clip = mainMenuMusic;
        music.Play();
    }

    public void PlayLevelWonTheme()
    {
        music.clip = levelWonMusic;
        music.Play();
    }

    public void PlayLevelTheme()
    {
        music.clip = levelMusic;
        music.Play();
    }

    public void PlayUI()
    {
        sound.clip = UIclick;
        sound.Play();
    }

    public void PlayCorrectDot()
    {
        sound.clip = correctDotClick;
        sound.Play();
    }

    public void PlayWrongDot()
    {
        sound.clip = wrongDotClick;
        sound.Play();
    }

}
