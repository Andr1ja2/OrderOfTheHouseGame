using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public string[] lines;

    [SerializeField] bool oneTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.dialogueController.StartDialogue((string[])lines.Clone()); ;
            if (oneTime) Destroy(gameObject);
        }
    }
}
