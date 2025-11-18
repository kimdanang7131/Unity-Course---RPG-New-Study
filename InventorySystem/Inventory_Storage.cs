using System.Collections.Generic;
using UnityEngine;

public class Inventory_Storage : Inventory_Base
{
    public Inventory_Player playerInventory { get; private set; }
    public List<Inventory_Item> materialStash;

    public void ConsumeMaterials(Inventory_Item itemToCraft)
    {
        foreach (var requiredItem in itemToCraft.itemData.craftRecipe)
        {
            int amountToConsume = requiredItem.stackSize;

            // 플레이어 인벤토리 먼저
            amountToConsume -= ConsumeMaterialsAmount(playerInventory.itemList, requiredItem);

            // 그다음 창고 인벤토리
            if (amountToConsume > 0)
                amountToConsume -= ConsumeMaterialsAmount(itemList, requiredItem);

            // 마지막으로 재료 보관함
            if (amountToConsume > 0)
                amountToConsume -= ConsumeMaterialsAmount(materialStash, requiredItem);
        }
    }

    private int ConsumeMaterialsAmount(List<Inventory_Item> itemList, Inventory_Item neededItem)
    {
        int amountNeeded = neededItem.stackSize;
        int consumedAmount = 0;

        foreach (var item in itemList)
        {
            if (item.itemData != neededItem.itemData)
                continue;

            int removeAmount = Mathf.Min(item.stackSize, amountNeeded - consumedAmount);
            item.stackSize -= removeAmount;
            consumedAmount += removeAmount;

            if (item.stackSize <= 0)
                itemList.Remove(item);

            if (consumedAmount >= neededItem.stackSize)
                break;
        }

        return consumedAmount;
    }

    public bool HasEnoughMaterials(Inventory_Item itemToCraft)
    {
        foreach (var requiredMaterial in itemToCraft.itemData.craftRecipe)
        {
            if (GetAvailableAmountOf(requiredMaterial.itemData) < requiredMaterial.stackSize)
                return false;
        }

        return true;
    }

    public int GetAvailableAmountOf(ItemDataSO requiredItem)
    {
        int amount = 0;

        foreach (var item in playerInventory.itemList)
        {
            if (item.itemData == requiredItem)
                amount = amount + item.stackSize;
        }

        foreach (var item in itemList)
        {
            if (item.itemData == requiredItem)
                amount = amount + item.stackSize;
        }


        foreach (var item in materialStash)
        {
            if (item.itemData == requiredItem)
                amount = amount + item.stackSize;
        }

        return amount;
    }

    public void AddMaterialToStash(Inventory_Item item)
    {
        var stackableItem = StackableInStash(item);

        if (stackableItem != null)
            stackableItem.AddStack();
        else
            materialStash.Add(item);

        TriggerUpdateUI();
    }

    public Inventory_Item StackableInStash(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = materialStash.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach (var stackableItem in stackableItems)
        {
            if (stackableItem.CanAddStack())
                return stackableItem;
        }

        return null;
    }

    public void SetInventory(Inventory_Player inventory) => this.playerInventory = inventory;

    public void FromPlayerToStorage(Inventory_Item item, bool transferFullStack)
    {
        int transferAmount = transferFullStack ? item.stackSize : 1;

        for (int i = 0; i < transferAmount; i++)
        {
            // 내아이템창( 창고 ) 에 아이템 추가 가능여부 확인
            if (CanAddItem(item))
            {
                var itemToAdd = new Inventory_Item(item.itemData);

                playerInventory.RemoveOneItem(item);
                AddItem(itemToAdd);
            }
        }

        TriggerUpdateUI();
    }

    public void FromStorageToPlayer(Inventory_Item item, bool transferFullStack)
    {
        int transferAmount = transferFullStack ? item.stackSize : 1;

        for (int i = 0; i < transferAmount; i++)
        {
            // 플레이어 인벤토리에 아이템 추가 가능여부 확인
            if (playerInventory.CanAddItem(item))
            {
                var itemToAdd = new Inventory_Item(item.itemData);

                RemoveOneItem(item);
                playerInventory.AddItem(itemToAdd);
            }
        }

        TriggerUpdateUI();
    }
}
