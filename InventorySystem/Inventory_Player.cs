using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    public int gold = 10000;

    private Player player;
    public List<Inventory_EquipmentSlot> equipList;

    public Inventory_Storage storage;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        storage = FindFirstObjectByType<Inventory_Storage>();
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

        UnequipItem(itemToUnequip, slotToReplace != null);
        EquipItem(inventoryItem, slotToReplace);
    }

    // EquipmentSlot에 장착하고, 인벤토리에서는 제거한다
    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        float savedHealthPercent = player.health.GetHealthPercent();

        slot.equippedItem = itemToEquip;
        slot.equippedItem.AddModifiers(player.stats);
        slot.equippedItem.AddItemEffect(player);

        player.health.SetHealthPercent(savedHealthPercent);
        RemoveOneItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnequip, bool replacingItem = false)
    {
        if (CanAddItem(itemToUnequip) == false)
        {
            Debug.Log("No Space");
            return;
        }

        float savedHealthPercent = player.health.GetHealthPercent();

        var slotToUnequip = equipList.Find(slot => slot.equippedItem == itemToUnequip);
        if (slotToUnequip != null)
            slotToUnequip.equippedItem = null;

        itemToUnequip.RemoveModifiers(player.stats);
        itemToUnequip.RemoveItemEffect();

        player.health.SetHealthPercent(savedHealthPercent);
        AddItem(itemToUnequip);
    }
}
