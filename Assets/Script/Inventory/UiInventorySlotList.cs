using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UiInventorySlotList : MonoBehaviour
{

    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,

    }

    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
    }

    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs,rhs) =>lhs.CreateItemTime.CompareTo(rhs.CreateItemTime),
        (lhs,rhs) =>rhs.CreateItemTime.CompareTo(lhs.CreateItemTime),
        (lhs,rhs) =>lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs,rhs) =>rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs,rhs) =>lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs,rhs) =>rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
    };

    public readonly Func<SaveItemData, bool>[] filterings =
    {
        (item) => true,
        (item) => item.ItemData.Type == Defines.ItemTypes.Weapon,
        (item) => item.ItemData.Type == Defines.ItemTypes.Equip,
        (item) => item.ItemData.Type == Defines.ItemTypes.Consumable,
        (item) => item.ItemData.Type != Defines.ItemTypes.Consumable,
    };

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;
    private int selectedSlotIndex = -1;


    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    public UiInventorySlot prefabInventorySlot;
    public ScrollRect scrollRect;
    public List<UiInventorySlot> uiSlotList = new();
    public List<SaveItemData> saveItemDataList = new();
    public int uiSlotMaxCount = 50;
    public UiItemInfo itemInfo;


    private void OnEnable()
    {
        onSelectSlot.AddListener(OnSelectSlot);
        SetSaveItemDataList(SaveLoadManager.Data.items);

        if(itemInfo != null)
        if (saveItemDataList.Count > 0)
        {
            itemInfo.SetItem(saveItemDataList[0]);
            selectedSlotIndex = 0;
        }
    }

    private void OnDisable()
    {
        onSelectSlot.RemoveListener(OnSelectSlot);
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefabInventorySlot, scrollRect.content);
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);
                newSlot.slotIndex = i;
                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveItemData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].SetItem(list[i]);
                uiSlotList[i].gameObject.SetActive(true);
            }
            else
            {
                uiSlotList[i].SetEmpty();
                uiSlotList[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].SetEmpty();
                uiSlotList[i].gameObject.SetActive(false);
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    private void OnSelectSlot(SaveItemData itemData)
    {
        Debug.Log($"Selected Item: {itemData.ToString()}");
        if (itemInfo != null)
            itemInfo.SetItem(itemData);
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        UpdateSlots();
    }

    public void RemoveItem()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();



        if (itemInfo != null)
            itemInfo.SetEmpty();
    }
}
