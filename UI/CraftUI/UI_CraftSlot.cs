using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftSlot : MonoBehaviour
{
    private ItemDataSO itemToCraft;
    [SerializeField] private UI_CraftPreview craftPreview;

    [SerializeField] private Image craftIcon;
    [SerializeField] private TextMeshProUGUI craftItemName;

    public void SetupButton(ItemDataSO craftData)
    {
        this.itemToCraft = craftData;
        craftIcon.sprite = craftData.itemIcon;
        craftItemName.text = craftData.itemName;
    }

    public void UpdateCraftPreview() => craftPreview.UdpateCraftPreview(itemToCraft);
}
