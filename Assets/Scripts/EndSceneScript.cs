using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    public IEnumerator SetScene()
    {
        yield return new WaitForSecondsRealtime(1);
        AudioController.instance.PlaySFX(AudioController.instance.endSceneClip);
        yield return new WaitForSecondsRealtime(9);
        FadeEffectAnimator.instance.animator.speed /= 2;
        FadeEffectAnimator.instance.FadeIn();
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length * 2);
        SceneManager.LoadScene("MainMenuScene");
    }

    private void Start()
    {
        StartCoroutine(SetScene());
    }
}
