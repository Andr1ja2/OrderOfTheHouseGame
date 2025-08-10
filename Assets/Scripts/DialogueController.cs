using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject DialogueBox;
    public Dialogue dialogue;

    private void Awake()
    {
        dialogue = DialogueBox.GetComponent<Dialogue>();
    }

    public bool DialogueIsActive()
    {
        return DialogueBox.GetComponent<Image>().color.a != 0 || GameManager.instance.levelController.noteCanvas.activeSelf;
    }

    public void StartDialogue(string[] lines)
    {
        if (lines.Length > 0 && !Array.Exists(lines, element => element == string.Empty))
        {
            dialogue.lines = lines;
            dialogue.Activate();
        }
    }
}
