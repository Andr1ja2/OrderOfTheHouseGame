using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndScript : MonoBehaviour
{
    [SerializeField] string[] linesNoKey;
    [SerializeField] string[] linesNoClothes;

    private IEnumerator EndLevel()
    {
        InputController.actionsEnabled = false;
        FadeEffectAnimator.instance.FadeIn();
        yield return new WaitForSecondsRealtime(FadeEffectAnimator.instance.animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("EndScene");
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
