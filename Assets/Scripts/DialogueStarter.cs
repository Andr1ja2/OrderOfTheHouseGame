using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public string[] lines;

    BoxCollider2D collider2d;
    [SerializeField] bool oneTime;

    private void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        GameManager.instance.ResetValues += ResetValues;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.dialogueController.StartDialogue((string[])lines.Clone());
            if (oneTime) collider2d.enabled = false;
        }
    }

    public void ResetValues()
    {
        if (collider2d != null)
            collider2d.enabled = true;
    }
}
