
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterList : MonoBehaviour
{
    public enum Filterings
    {
        None,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }

    public enum Sortings
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        LevelAscending,
        LevelDescending,
    }

    public Comparer<SaveCharacterData>[] comparisons =
    {
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>lhs.CreateItemTime.CompareTo(rhs.CreateItemTime)),
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>rhs.CreateItemTime.CompareTo(lhs.CreateItemTime)),
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName)),
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName)),
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>lhs.CharacterData.Level.CompareTo(rhs.CharacterData.Level)),
        Comparer<SaveCharacterData>.Create((lhs,rhs) =>rhs.CharacterData.Level.CompareTo(lhs.CharacterData.Level)),
    };

    public Func<SaveCharacterData, bool>[] filterings =
    {
        (character) => true,
        (character) => character.CharacterData.Level == 1,
        (character) => character.CharacterData.Level == 2,
        (character) => character.CharacterData.Level == 3,
        (character) => character.CharacterData.Level == 4,
        (character) => character.CharacterData.Level == 5,
    };

    private Filterings _filterings = Filterings.None;
    private Sortings _sortings = Sortings.CreationTimeAscending;

    public Filterings Filter
    {
        get => _filterings;
        set
        {
            if (_filterings != value)
            {
                _filterings = value;
                UpdateSlots();
            }
        }
    }

    public Sortings Sorting
    {
        get => _sortings;
        set
        {
            if (_sortings != value)
            {
                _sortings = value;
                UpdateSlots();
            }
        }
    }

    public ScrollRect scrollRect;
    public UiCharacterSlot slotPrefab;

    private List<UiCharacterSlot> slots = new List<UiCharacterSlot>();
    public List<SaveCharacterData> saveCharacterDatas = new List<SaveCharacterData>();

    public int selectedSlotIndex = -1;

    public UnityEvent<SaveCharacterData> onSelectSlot;

    public void UpdateSlots()
    {
        var list = saveCharacterDatas.Where(filterings[(int)Filter]).ToList();
        list.Sort(comparisons[(int)Sorting]);

        for (int i = slots.Count; i < list.Count; i++)
        {
            var slot = Instantiate(slotPrefab, scrollRect.content);
            slot.slot = i;
            slot.button.onClick.AddListener(() => OnSlotClicked(slot));
            slots.Add(slot);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < list.Count)
            {
                slots[i].Character = list[i];
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].SetEmpty();
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> list)
    {
        saveCharacterDatas = list.ToList();
        UpdateSlots();
    }


    public void Save()
    {
        SaveLoadManager.Data.characters = saveCharacterDatas;
        SaveLoadManager.Save();
    }

    public void Load()
    {
        SaveLoadManager.Load();
        SetSaveCharacterDataList(SaveLoadManager.Data.characters);
    }

    public void AddRandomCharacter()
    {
        saveCharacterDatas.Add(SaveCharacterData.GetRandomCharacter());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharacterDatas.Remove(slots[selectedSlotIndex].Character);
        UpdateSlots();
    }

    public void OnSlotClicked(UiCharacterSlot slot)
    {
        selectedSlotIndex = slot.slot;
        onSelectSlot?.Invoke(slot.Character);
    }
}
