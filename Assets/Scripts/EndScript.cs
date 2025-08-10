using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScript : MonoBehaviour
{
    [SerializeField] string[] linesNoKey;
    [SerializeField] string[] linesNoClothes;

    private void Start()
    {
        GameManager.instance.ResetValues += OnResetValues;
    }

    private void OnResetValues()
    {
        var temp = GameManager.instance.PushAction -= OpenDoor;
    }

    private IEnumerator EndLevel()
    {
        InputController.actionsEnabled = false;
        FadeEffectAnimator.instance.FadeIn();
        AudioController.instance.PlaySFX(AudioController.instance.doorUnlock);
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("EndScene");
        //FadeEffectAnimator.instance.FadeOut();
    }

    private void OpenDoor()
    {
        if (GameManager.instance.inventoryController.hasKey)
        {
            if (GameManager.instance.inventoryController.hasBoots && GameManager.instance.inventoryController.hasJacket)
            {
                StartCoroutine(EndLevel());
            }
            else
            {
                GameManager.instance.dialogueController.StartDialogue((string[])linesNoClothes.Clone());
            }
        }
        else
        {
            AudioController.instance.PlaySFX(AudioController.instance.doorLocked);
            GameManager.instance.dialogueController.StartDialogue((string[])linesNoKey.Clone());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction += OpenDoor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= OpenDoor;
        }
    }
}
