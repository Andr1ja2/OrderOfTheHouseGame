using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    public Animator fadeEffectAnimator;

    public IEnumerator SetScene()
    {
        yield return new WaitForSecondsRealtime(10);
        fadeEffectAnimator.speed /= 2;
        fadeEffectAnimator.SetTrigger("fadeIn");
        yield return new WaitForSecondsRealtime(fadeEffectAnimator.GetCurrentAnimatorStateInfo(0).length * 2);
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Start()
    {
        StartCoroutine(SetScene());
    }
}
