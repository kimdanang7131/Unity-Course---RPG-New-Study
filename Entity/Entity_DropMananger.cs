using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity_DropMananger : MonoBehaviour
{
    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private ItemListDataSO dropData;

    [Header("Drop restrctions")]
    [SerializeField] private int maxRarityAmount = 1200;
    [SerializeField] private int maxItemsToDrop = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DropItems();
    }

    public virtual void DropItems()
    {
        if (dropData == null)
        {
            Debug.Log("No Drop Data assigned!");
            return;
        }

        List<ItemDataSO> itemToDrop = RollDrops();
        int amountToDrop = Mathf.Min(itemToDrop.Count, maxItemsToDrop);

        for (int i = 0; i < amountToDrop; i++)
        {
            CreateItemDrop(itemToDrop[i]);
        }
    }

    protected void CreateItemDrop(ItemDataSO itemToDrop)
    {
        GameObject newItem = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        newItem.GetComponent<Object_ItemPickup>().SetupItem(itemToDrop);
    }

    public List<ItemDataSO> RollDrops()
    {
        List<ItemDataSO> possibleDrops = new List<ItemDataSO>();
        List<ItemDataSO> finalDrops = new List<ItemDataSO>();
        float maxRarityAmount = this.maxRarityAmount;

        // 일단 아이템 rarity를 통과한 녀석들을 뽑음
        foreach (var item in dropData.itemList)
        {
            float dropChance = item.GetDropChance();

            if (Random.Range(0, 100) <= dropChance)
                possibleDrops.Add(item);
        }

        // 내림차순 정렬 - 희귀도 좋은 순으로 뿌려주기
        possibleDrops = possibleDrops.OrderByDescending(item => item.itemRarity).ToList();

        // 최대 희귀도 설정값 내에서 item.Rarity를 빼가며 drops아이템 뽑기
        foreach (var item in possibleDrops)
        {
            if (maxRarityAmount > item.itemRarity)
            {
                finalDrops.Add(item);
                maxRarityAmount = maxRarityAmount - item.itemRarity;
            }
        }

        return finalDrops;
    }
}
