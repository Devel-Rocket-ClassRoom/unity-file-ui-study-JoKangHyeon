using System;
using System.Collections.Generic;

[Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        SaveDataV2 saveData = new SaveDataV2();

        saveData.Name = PlayerName;
        saveData.Gold = 0;

        return saveData;
    }
}

[Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        SaveDataV3 saveData = new SaveDataV3();

        saveData.Name = Name;
        saveData.Gold = Gold;
        saveData.items = new();

        return saveData;
    }
}


[Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> items = new();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        SaveDataV4 saveData = new SaveDataV4();
        saveData.Name = Name;
        saveData.Gold = Gold;

        foreach (var id in items)
        {
            SaveItemData data = new();
            data.ItemData = DataTableManager.ItemTable.Get(id);

            saveData.items.Add(data);
        }

        return saveData;
    }
}

[Serializable]
public class SaveDataV4 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<SaveItemData> items = new();

    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveData VersionUp()
    {
        SaveDataV5 saveData = new();
        saveData.Name = Name;
        saveData.Gold = Gold;
        saveData.items = items;

        return saveData;
    }
}

[Serializable]
public class SaveDataV5 : SaveDataV4
{
    public UiInventorySlotList.SortingOptions Sorting { get; set; } = UiInventorySlotList.SortingOptions.CreationTimeAscending;
    public UiInventorySlotList.FilteringOptions Filtering { get; set; } = UiInventorySlotList.FilteringOptions.None;

    public SaveDataV5()
    {
        Version = 5;
    }

    public override SaveData VersionUp()
    {
        SaveDataV6 saveData = new();
        saveData.Name = Name;
        saveData.Gold = Gold;
        saveData.items = items;
        saveData.Sorting = Sorting;
        saveData.Filtering = Filtering;
        return saveData;
    }
}

[Serializable]
public class SaveDataV6 : SaveDataV5
{
    public List<SaveCharacterData> characters = new();

    public SaveDataV6()
    {
        Version = 6;
    }

    public override SaveData VersionUp()
    {
        throw new NotImplementedException();
    }
}