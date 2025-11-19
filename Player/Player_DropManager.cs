using System.Collections.Generic;
using UnityEngine;

public class Player_DropManager : Entity_DropMananger
{
    [Header("Player Drop Details")]
    [Range(0, 100f)]
    [SerializeField] private float chanceToLoseItem = 90f;
    private Inventory_Player playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<Inventory_Player>();
    }

    public override void DropItems()
    {
        List<Inventory_Item> inventoryCopy = new List<Inventory_Item>(playerInventory.itemList);
        List<Inventory_EquipmentSlot> equipCopy = new List<Inventory_EquipmentSlot>(playerInventory.equipList);

        foreach (var item in inventoryCopy)
        {
            if (Random.Range(0, 100) < chanceToLoseItem)
            {
                CreateItemDrop(item.itemData);
                playerInventory.RemoveFullStack(item);
            }
        }

        foreach (var equip in equipCopy)
        {
            if (Random.Range(0, 100) < chanceToLoseItem && equip.HasItem())
            {
                var item = equip.GetEquippedItem();

                CreateItemDrop(item.itemData);
                playerInventory.UnequipItem(item);
                playerInventory.RemoveFullStack(item);
            }
        }
    }
}
