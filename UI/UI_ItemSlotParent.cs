using System.Collections.Generic;
using UnityEngine;

public class UI_ItemSlotParent : MonoBehaviour
{
    private UI_ItemSlot[] slots;

    public void UpdateSlots(List<Inventory_Item> itemList)
    {
        if (slots == null)
            slots = GetComponentsInChildren<UI_ItemSlot>();

        // 총 10개의 ui Item 슬롯은 고정
        for (int i = 0; i < slots.Length; i++)
        {
            // uiItemSlot은 고정이지만,  [itemList는 가변적이므로 itemList까지만 순회] 
            if (i < itemList.Count)
            {
                slots[i].UpdateSlot(itemList[i]);
            }
            else
            {
                slots[i].UpdateSlot(null);
            }
        }
    }
}
