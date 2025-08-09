using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public string[] pickupLines; // ;)

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
        if (this.gameObject.CompareTag("Boots"))
        {
            GameManager.instance.inventoryController.hasBoots = true;
            GameManager.instance.dialogueController.StartDialogue((string[])pickupLines.Clone());
        }
        else if (this.gameObject.CompareTag("Jacket"))
        {
            GameManager.instance.inventoryController.hasJacket = true;
            GameManager.instance.dialogueController.StartDialogue((string[])pickupLines.Clone());
        }
        Destroy(gameObject);
    }
}
