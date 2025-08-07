using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public string[] lines;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.dialogueController.dialogue.lines = (string[])lines.Clone(); ;
            GameManager.instance.dialogueController.dialogue.Activate();
            Destroy(gameObject);
        }
    }
}
