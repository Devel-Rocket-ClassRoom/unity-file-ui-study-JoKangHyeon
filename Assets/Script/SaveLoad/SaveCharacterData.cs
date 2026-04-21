using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterDataConverter : JsonConverter<CharacterData>
{
    public override CharacterData ReadJson(JsonReader reader, Type objectType, CharacterData existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string id = (string)reader.Value;
        return DataTableManager.CharacterTable.Get(id);
    }
    public override void WriteJson(JsonWriter writer, CharacterData value, JsonSerializer serializer)
    {
        writer.WriteValue(value.Id);
    }
}


[Serializable]
public class SaveCharacterData
{
    public Guid InstanceId { get; set; }
    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }
    public DateTime CreateItemTime { get; set; }
    public List<SaveItemData> Equipments { get; set; } = new();

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreateItemTime = DateTime.Now;
        Equipments = new();
    }

    public static SaveCharacterData GetRandomCharacter()
    {
        int randomCharacter = UnityEngine.Random.Range(0, DataTableManager.CharacterTable.table.Count);
        var character = DataTableManager.CharacterTable.table.Values.ToList()[randomCharacter];
        var characterSaveData = new SaveCharacterData();
        characterSaveData.CharacterData = character;
        characterSaveData.Equipments.Add(null);
        characterSaveData.Equipments.Add(null);
        return characterSaveData;
    }
    public override string ToString()
    {
        return $"SaveItemData : {InstanceId} / {CreateItemTime} / {CharacterData}";
    }

}