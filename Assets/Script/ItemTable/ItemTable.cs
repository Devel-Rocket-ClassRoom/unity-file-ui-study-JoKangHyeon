using System.Collections.Generic;
using UnityEngine;

public class ItemTable : DataTable
{
    Dictionary<string, ItemData> table = new();

    public override void Load(string fileName)
    {
        table.Clear();

        var path = string.Format(DataTable.FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        List<ItemData> list = LoadCsv<ItemData>(textAsset.text);
        
        foreach(ItemData item in list)
        {
            if (table.ContainsKey(item.Name))
            {
                Debug.LogError($"아이템 아이디 중복: {item}");
                continue;
            }

            table[item.Id] = item;
        }
    }

    public ItemData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            return null;
        }

        return table[id];
    }
}

public class ViewableData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}

public class ItemData : ViewableData
{
    public Defines.ItemTypes Type { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }


    public override string ToString()
    {
        return $"{Id} / {Type} / {StringName} / {StringDesc} / {Value} / {Cost} / {Icon}";
    }
}