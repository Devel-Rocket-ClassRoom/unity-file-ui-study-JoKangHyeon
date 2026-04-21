using Newtonsoft.Json;
using System;
using System.Linq;

public class ItemDataConverter : JsonConverter<ItemData>
{
    public override ItemData ReadJson(JsonReader reader, Type objectType, ItemData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = (string)reader.Value;
        return DataTableManager.ItemTable.Get(id);
    }
    public override void WriteJson(JsonWriter writer, ItemData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}

[Serializable]
public class SaveItemData
{
    public Guid InstanceId { get; set; }
    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }
    public DateTime CreateItemTime { get; set; }

    public SaveItemData()
    {
        InstanceId = Guid.NewGuid();
        CreateItemTime = DateTime.Now;
    }

    public static SaveItemData GetRandomItem()
    {
        int randomItem = UnityEngine.Random.Range(0, DataTableManager.ItemTable.table.Count);
        var item = DataTableManager.ItemTable.table.Values.ToList()[randomItem];
        var itemSaveData = new SaveItemData();
        itemSaveData.ItemData = item;
        return itemSaveData;
    }
    public override string ToString()
    {
        return $"SaveItemData : {InstanceId} / {CreateItemTime} / {ItemData}";
    }
}
