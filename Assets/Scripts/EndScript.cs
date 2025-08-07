using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField] string[] linesNoKey;
    [SerializeField] string[] linesNoClothes;

    private void EndLevel()
    {
        Debug.Log("won");
    }

    private void OpenDoor()
    {
        if (GameManager.instance.inventoryController.hasKey)
        {
            if (GameManager.instance.inventoryController.hasBoots && GameManager.instance.inventoryController.hasJacket)
            {
                EndLevel();
            }
            else
            {
                GameManager.instance.dialogueController.dialogue.lines = (string[])linesNoClothes.Clone();
                GameManager.instance.dialogueController.dialogue.Activate();
            }
        }
        else
        {
            GameManager.instance.dialogueController.dialogue.lines = (string[])linesNoKey.Clone();
            GameManager.instance.dialogueController.dialogue.Activate();
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
