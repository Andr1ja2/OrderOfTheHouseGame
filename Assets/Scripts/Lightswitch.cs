using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightswitch : MonoBehaviour
{
    public string[] ligthsOnLines;

    [SerializeField] Image Darkness;

    private void Start()
    {
        GameManager.instance.ResetValues += ResetValues;
    }

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

        GameManager.instance.dialogueController.StartDialogue((string[])ligthsOnLines.Clone());
    }

    public void ResetValues()
    {
        var tempColor = Darkness.color;
        tempColor.a = 0.8f;
        Darkness.color = tempColor;
    }
}
