using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public string[] pickupKeyLines;

    GameObject key;
    public  bool canCollectKey;
    public bool canClimbChair;

    Vector3 startposition;

    private void Start()
    {
        GameManager.instance.PushAction += CollectKey;

        startposition = transform.position;
        GameManager.instance.ResetValues += ResetValues;
    }

    private void CollectKey()
    {
        if (canCollectKey && canClimbChair)
        {
            AudioController.instance.PlaySFX(AudioController.instance.pickupKey);

            GameManager.instance.inventoryController.PickedUpKey();
            Destroy(GameObject.FindGameObjectWithTag("Key"));
            GameManager.instance.dialogueController.StartDialogue((string[])pickupKeyLines.Clone());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            canCollectKey = true;
        }
        if (collision.transform.CompareTag("Player"))
        {
            canClimbChair = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            canCollectKey = false;
        }
        if (collision.transform.CompareTag("Player"))
        {
            canClimbChair = false;
        }
    }

    public void ResetValues()
    {
        canCollectKey = false;
        canClimbChair = false;
        transform.position = startposition;
    }
}
