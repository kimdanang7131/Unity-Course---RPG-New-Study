using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{
    public event Action OnInventoryChange;

    public int maxInventorySize = 10;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();

    protected virtual void Awake()
    {

    }

    public void TryUseItem(Inventory_Item itemToUse)
    {
        Inventory_Item consumable = itemList.Find(item => item == itemToUse);

        if (consumable == null)
            return;

        consumable.itemEffect.ExecuteEffect();

        if (consumable.stackSize > 1)
            consumable.RemoveStack();
        else
            RemoveOneItem(consumable);

        OnInventoryChange?.Invoke();
    }

    public bool CanAddItem(Inventory_Item itemToAdd)
    {
        bool hasStakable = StackableItem(itemToAdd) != null;
        return hasStakable || itemList.Count < maxInventorySize;
    }

    public Inventory_Item StackableItem(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach (var stackableItem in stackableItems)
        {
            if (stackableItem.CanAddStack())
                return stackableItem;
        }

        return null;
    }

    // List에 아이템이 없다면 새로생성, 이미 있다면 스택 추가
    public void AddItem(Inventory_Item itemToAdd)
    {
        Inventory_Item existingStackable = StackableItem(itemToAdd);

        if (existingStackable != null)
            existingStackable.AddStack();
        else
            itemList.Add(itemToAdd);

        OnInventoryChange?.Invoke();
    }

    public void RemoveOneItem(Inventory_Item itemToRemove)
    {
        Inventory_Item itemInventory = itemList.Find(item => item == itemToRemove);

        if (itemInventory.stackSize > 1)
            itemToRemove.RemoveStack();
        else
            itemList.Remove(itemToRemove);

        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItem(ItemDataSO itemData)
    {
        return itemList.Find(item => item.itemData == itemData);
    }

    public void TriggerUpdateUI() => OnInventoryChange?.Invoke();
}
