using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public bool hasKey;
    public bool hasBoots;
    public bool hasJacket;

    public List<Image> placeHolders;

    public Sprite keySprite;
    public Sprite jacketSprite;
    public Sprite bootsSprite;

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

        foreach (Image im in placeHolders)
        {
            im.sprite = null;
            im.enabled = false;
        }
    }

    public void PickedUpKey()
    {
        hasKey = true;
        PlaceSprite(keySprite);
    }

    public void PickedUpJacket()
    {
        hasJacket = true;
        PlaceSprite(jacketSprite);
    }

    public void PickedUpBoots()
    {
        hasBoots = true;
        PlaceSprite(bootsSprite);
    }

    public void PlaceSprite(Sprite image)
    {
        foreach(Image im in placeHolders)
        {
            if (im.sprite != null) continue;

            im.sprite = image;
            im.enabled = true;
            break;
        }
    }

}
