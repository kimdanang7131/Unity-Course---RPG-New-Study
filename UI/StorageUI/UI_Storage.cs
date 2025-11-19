using UnityEngine;

public class UI_Storage : MonoBehaviour
{
    private Inventory_Player playerInventory;
    private Inventory_Storage storage;

    [SerializeField] private UI_ItemSlotParent inventoryParent;
    [SerializeField] private UI_ItemSlotParent stoargeParent;
    [SerializeField] private UI_ItemSlotParent materialStashParent;

    public void SetupStorageUI(Inventory_Storage storage)
    {
        this.storage = storage;
        this.playerInventory = storage.playerInventory;

        storage.OnInventoryChange += UpdateUI;
        UpdateUI();

        UI_StorageSlot[] storageSlots = GetComponentsInChildren<UI_StorageSlot>();

        foreach (var slot in storageSlots)
            slot.SetStorage(storage);
    }

    void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (storage == null)
            return;

        inventoryParent.UpdateSlots(playerInventory.itemList);
        stoargeParent.UpdateSlots(storage.itemList);
        materialStashParent.UpdateSlots(storage.materialStash);
    }
}
