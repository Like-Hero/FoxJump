using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory Data")]
[Serializable]
public class InventoryData_SO : ScriptableObject
{
    [SerializeField]
    public List<Item> AllItems;
    public bool CalculateItemCount(int itemId, int value)
    {
        Item item = FindItem(itemId);
        if (item != null)
        {
            int resultValue = item.itemHeld + value;
            if(resultValue < 0)
            {
                return false;
            }
            else
            {
                item.itemHeld = resultValue;
                return true;
            }
        }
        return false;
    }
    public bool CalculateItemCount(Item item, int value)
    {
        return CalculateItemCount(item.itemId, value);
    }
    public bool CalculateItemCount(string itemName, int value)
    {
        return CalculateItemCount(FindItem(itemName), value);
    }
    public bool CalculateItemCount(ItemType itemType, int value)
    {
        return CalculateItemCount(FindItem(itemType), value);
    }
    public Item GetItem(int itemId)
    {
        return FindItem(itemId);
    }
    public Item GetItem(string itemName)
    {
        return FindItem(itemName);
    }
    public Item GetItem(ItemType itemType)
    {
        return FindItem(itemType);
    }
    private Item FindItem(int itemId)
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i] != null && itemId == AllItems[i].itemId)
            {
                return AllItems[i];
            }
        }
        return null;
    }
    private Item FindItem(string itemName)
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i] != null && itemName == AllItems[i].itemName)
            {
                return AllItems[i];
            }
        }
        return null;
    }
    private Item FindItem(ItemType itemType)
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i] != null && itemType == AllItems[i].itemType)
            {
                return AllItems[i];
            }
        }
        return null;
    }
}
