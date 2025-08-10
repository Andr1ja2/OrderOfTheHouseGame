using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    Color invisible;
    Image dialogueboxImage;

    private void Awake()
    {
        dialogueboxImage = GetComponent<Image>();
        invisible = dialogueboxImage.color;
    }

    private void Start()
    {
        GameManager.instance.SkipDialogue += Skip;

        textComponent.text = string.Empty;
    }

    public void Activate()
    {
        if (lines.Length > 0)
        {
            invisible.a = 0.7f;
            dialogueboxImage.color = invisible;
            InputController.actionsEnabled = false;
            StartDialogue();
        }
    }

    private void Skip()
    {
        if (dialogueboxImage.color.a != 0)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return null; // Pause 1 frame so UI can load before dialogue box starts typing
        foreach (char c in lines[index].ToCharArray())
        {
            AudioController.instance.PlaySFX(AudioController.instance.letterClick);
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else // disabling dialoguebox using alpha because game lags when using SetActive for some reason
        {
            invisible.a = 0;
            dialogueboxImage.color = invisible;
            textComponent.text = string.Empty;
            Array.Clear(lines, 0, lines.Length);
            InputController.actionsEnabled = true;
        }
    }
}
