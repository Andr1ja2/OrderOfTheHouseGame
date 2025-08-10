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
        sfxSource.PlayOneShot(clip);
    }


    // Below code makes object a singleton that gets replaced in the next scene
    // if there already is one so that I can have different ones where I need
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioController sceneInstance = FindObjectOfType<AudioController>();
        if (sceneInstance != null && sceneInstance != instance)
        {
            Destroy(instance.gameObject);
            instance = sceneInstance;
        }
    }
}
