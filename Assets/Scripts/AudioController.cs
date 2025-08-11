using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] AudioMixer audioMixer;

    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource clockSource;
    [SerializeField] AudioSource sfxSource;

    [Header("----------Audio Clip----------")]
    public AudioClip backgroundMusic;
    public AudioClip backgroundClock;
    public AudioClip pushJunk;
    public AudioClip noPushJunk;
    public AudioClip pickupKey;
    public AudioClip pickupItem;
    public AudioClip death;
    public AudioClip doorUnlock;
    public AudioClip doorLocked;
    public AudioClip lightSwitch;
    public AudioClip pickupNote;
    public AudioClip openBoard;
    public AudioClip clockInteract;
    public AudioClip letterClick;
    public AudioClip endSceneClip;
    //clock
    //

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }

        if (musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        if (clockSource != null)
        {
            clockSource.clip = backgroundClock;
            clockSource.loop = true;
            clockSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }


    // Making a DontDestroyOnLoad object that swaps with a new one if there is one in the loeaded scene
    // because I need a singleton with different values based on scene
    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
