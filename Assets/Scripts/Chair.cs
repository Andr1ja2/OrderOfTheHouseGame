using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public string[] firstChairLines;

    JunkScript script;
    GameObject key;
    bool canCollectKey;
    bool canClimbChair;

    private void Start()
    {
        GameManager.instance.PushAction += CollectKey;
        key = GameObject.FindGameObjectWithTag("Key");
        script = GetComponent<JunkScript>();
    }

    private void CollectKey()
    {
        if (canCollectKey && canClimbChair)
        {
            GameManager.instance.inventoryController.hasKey = true;
            Destroy(key);
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
}
