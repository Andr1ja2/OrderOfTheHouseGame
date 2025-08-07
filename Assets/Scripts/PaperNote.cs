using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperNote : MonoBehaviour
{
    public string noteText;
    public int rulenumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.levelController.PickUpNote(this, rulenumber);
        }
    }
}
