using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeEffectAnimator : MonoBehaviour
{
    public static FadeEffectAnimator instance;
    public Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn()
    {
        animator.SetTrigger("fadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("fadeOut");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fadeIn"))
            FadeOut();
    }
}
