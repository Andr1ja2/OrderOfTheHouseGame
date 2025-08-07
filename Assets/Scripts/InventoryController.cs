using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public bool hasKey;
    public bool hasBoots;
    public bool hasJacket;

    private void Start()
    {
        ResetValues();
        GameManager.instance.ResetValues += ResetValues;
    }

    public void ResetValues()
    {
        hasKey = false;
        hasBoots = false;
        hasJacket = false;
    }
}
