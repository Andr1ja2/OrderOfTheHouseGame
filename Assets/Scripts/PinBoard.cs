using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoard : MonoBehaviour
{
    public bool hasRule1 = false;
    public bool hasRule2 = false;
    public bool hasRule3 = false;

    string pinBoardText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction += OpenPinBoard;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= OpenPinBoard;
        }
    }

    public void OpenPinBoard()
    {
        GameManager.instance.levelController.headerText.SetActive(true);
        pinBoardText = string.Empty;
        if (hasRule1)
        {
            pinBoardText += "First Rule: Don't go into the living room between midnight and 6 AM\n";
        }
        if (hasRule2)
        {
            pinBoardText += "Second Rule: Don't make more than 25 steps per room\n";
            // counter enable
        }
        if (hasRule3)
        {
            pinBoardText += "Third Rule: Don't walk on the carpets with your muddy boots\n";
        }

        if (pinBoardText != string.Empty)
        {
            GameManager.instance.levelController.noteTextUI.text = pinBoardText;
            GameManager.instance.levelController.noteCanvas.SetActive(true);
            InputController.actionsEnabled = false;
        }
        else
        {
            GameManager.instance.dialogueController.StartDialogue(new string[] { "Nothing to see here yet..." });
        }

    }
}
