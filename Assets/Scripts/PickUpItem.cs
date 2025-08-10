using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string[] pickupLines; // ;)

    private void Start()
    {
        GameManager.instance.ResetValues += OnValueReset;
    }

    private void OnValueReset()
    {
        GameManager.instance.PushAction -= Pickup;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction += Pickup;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PushAction -= Pickup;
        }
    }

    private void Pickup()
    {
        AudioController.instance.PlaySFX(AudioController.instance.pickupItem);
        if (this.gameObject.CompareTag("Boots"))
        {
            GameManager.instance.inventoryController.PickedUpBoots();
            GameManager.instance.dialogueController.StartDialogue((string[])pickupLines.Clone());
        }
        else if (this.gameObject.CompareTag("Jacket"))
        {
            GameManager.instance.inventoryController.PickedUpJacket();
            GameManager.instance.dialogueController.StartDialogue((string[])pickupLines.Clone());
        }
        Destroy(gameObject);
    }
}
