using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
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
        }
        else if (this.gameObject.CompareTag("Jacket"))
        {
            GameManager.instance.inventoryController.hasJacket = true;
        }
        Destroy(gameObject);
    }
}
