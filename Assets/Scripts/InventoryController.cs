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
        hasKey = false;
        hasBoots = false;
        hasJacket = false;
    }
}
