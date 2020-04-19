using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickup
{
    public Sprite itemSprite;
    public enum ItemType { LOG, CHOCOLATE, COAL }

    public ItemType type;
    private void Start()
    {
        itemSprite = this.GetComponent<SpriteRenderer>().sprite;
    }
    public Item Pickup()
    {
        return this;
    }
}
