using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public string[] startDialogue;
    public string[] pushDialogue;

    private void Start()
    {
        GameManager.instance.clock.stop = true;
        GameManager.instance.clock.hour = 3;
        GameManager.instance.PushAction += PushDialogue;

        GameManager.instance.dialogueController.StartDialogue((string[])startDialogue.Clone());
    }

    private void PushDialogue()
    {
        GameManager.instance.dialogueController.StartDialogue((string[])pushDialogue.Clone());
        GameManager.instance.PushAction -= PushDialogue;
    }
}
