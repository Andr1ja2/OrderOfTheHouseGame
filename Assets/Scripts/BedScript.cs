using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public string[] outOfBedLines; //ADD!
    int i = 0, j= 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            {
                GameManager.instance.PushAction += Sleep;
                Debug.Log(i++);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= Sleep;
            Debug.Log(j++);

        }
    }

    private void Sleep()
    {
        if (GameManager.instance.playerController.FacingDirection == Vector2.right)
            StartCoroutine(SleepRoutine());
    }

    private IEnumerator SleepRoutine()
    {
        InputController.actionsEnabled = false;
        FadeEffectAnimator.instance.FadeIn();
        GameManager.instance.clock.hour = 8;
        GameManager.instance.clock.minute = 0;
        PlayerController.stepCount = 0;
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        FadeEffectAnimator.instance.FadeOut();
        InputController.actionsEnabled = true;
        GameManager.instance.dialogueController.StartDialogue((string[])outOfBedLines.Clone());
    }

    private void OnDestroy()
    {
        GameManager.instance.PushAction -= Sleep;
    }
}
