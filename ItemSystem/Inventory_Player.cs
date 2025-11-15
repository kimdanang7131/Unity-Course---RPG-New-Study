using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Entity_Stats playerStats;
    public List<Inventory_EquipmentSlot> equipList;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<Entity_Stats>();
    }

    public void TryEquipItem(Inventory_Item item)
    {
        Inventory_Item inventoryItem = FindItem(item.itemData);
        List<Inventory_EquipmentSlot> matchingSlots = equipList.FindAll(slot => slot.slotType == item.itemData.itemType);

        // Step1. Try to find empty slot and equip item
        foreach (var slot in matchingSlots)
        {
            if (slot.HasItem() == false)
            {
                EquipItem(inventoryItem, slot);
                return;
            }
        }

        // Step2. No empty slots ? Replace first one
        var slotToReplace = matchingSlots[0];
        var itemToUnequip = slotToReplace.equippedItem;

        EquipItem(inventoryItem, slotToReplace);
        UnequipItem(itemToUnequip);
    }

    // EquipmentSlot에 장착하고, 인벤토리에서는 제거한다
    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        slot.equippedItem = itemToEquip;
        slot.equippedItem.AddModifiers(playerStats);

        RemoveItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnequip)
    {
        if (CanAddItem() == false)
        {
            Debug.Log("No Space");
            return;
        }

        foreach (var slot in equipList)
        {
            if (slot.equippedItem == itemToUnequip)
            {
                slot.equippedItem = null;
                break;
            }
        }

        itemToUnequip.RemoveModifiers(playerStats);
        AddItem(itemToUnequip);
    }
}
