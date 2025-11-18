using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/ItemList", fileName = "List of items - ")]
public class ItemListDataSO : ScriptableObject
{
    public ItemDataSO[] itemList;
}
