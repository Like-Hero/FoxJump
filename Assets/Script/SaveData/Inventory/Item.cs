using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Cherry,
    Gem,
    Money
}

[Serializable]
public class Item
{
    public int itemId;
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int itemHeld;
}
