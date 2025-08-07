using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightswitch : MonoBehaviour
{
    public string[] ligthsOnLines;

    [SerializeField] Image Darkness;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction += TurnOnLight;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= TurnOnLight;
        }
    }

    private void TurnOnLight()
    {
        var tempColor = Darkness.color;
        tempColor.a = 0;
        Darkness.color = tempColor;

        GameManager.instance.dialogueController.dialogue.lines = (string[])ligthsOnLines.Clone();
        GameManager.instance.dialogueController.dialogue.Activate();
    }
}
