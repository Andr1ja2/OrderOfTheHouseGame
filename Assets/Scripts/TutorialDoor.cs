using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialDoor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.instance.PushAction += EndTutorial;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameManager.instance.PushAction -= EndTutorial;
    }

    private void EndTutorial()
    {
        if (!(GameManager.instance.playerController.FacingDirection == Vector2.up)) return;
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        InputController.actionsEnabled = false;
        FadeEffectAnimator.instance.FadeIn();
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        PlayerPrefs.SetInt("TutorialDone", 1);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("HouseScene");
    }
}
