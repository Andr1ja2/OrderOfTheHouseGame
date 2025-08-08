using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    //public AudioClip other;
    //public AudioClip other;
    //public AudioClip other;

    //private void Awake()
    //{
    //    if (PlayerPrefs.HasKey("Volume"))
    //    {
    //        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
    //    }
    //}

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
