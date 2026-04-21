using TMPro;
using UnityEngine;

public class UiPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInventorySlotList inventorySlotList;

    void OnEnable()
    {
        OnLoad();
        
        if(filtering != null )
            filtering.value = (int)SaveLoadManager.Data.Filtering;
        if (sorting != null)
            sorting.value = (int)SaveLoadManager.Data.Sorting;
    }

    public void OnChangeSorting(int index)
    {
        inventorySlotList.Sorting = (UiInventorySlotList.SortingOptions)index;
        SaveLoadManager.Data.Sorting = (UiInventorySlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        inventorySlotList.Filtering = (UiInventorySlotList.FilteringOptions)index;
        SaveLoadManager.Data.Filtering = (UiInventorySlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.items = inventorySlotList.saveItemDataList;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        inventorySlotList.SetSaveItemDataList(SaveLoadManager.Data.items);
    }

    public void OnAddItem()
    {
        inventorySlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        inventorySlotList.RemoveItem();
    }
}
