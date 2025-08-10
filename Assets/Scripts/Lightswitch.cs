using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightswitch : MonoBehaviour
{
    public string[] ligthsOnLines;

    [SerializeField] Image Darkness;

    float initialDarkness;

    private void Start()
    {
        GameManager.instance.ResetValues += ResetValues;
        initialDarkness = Darkness.color.a;
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
        if (Darkness.color.a > 0)
        {
            AudioController.instance.PlaySFX(AudioController.instance.lightSwitch);

            var tempColor = Darkness.color;
            tempColor.a = 0;
            Darkness.color = tempColor;

            GameManager.instance.dialogueController.StartDialogue((string[])ligthsOnLines.Clone());
        }   
    }

    public void ResetValues()
    {
        var tempColor = Darkness.color;
        tempColor.a = initialDarkness;
        Darkness.color = tempColor;
    }
}
