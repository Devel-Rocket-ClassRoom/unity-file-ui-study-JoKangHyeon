using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CharacterData : ViewableData
{
    public int Level { get; set; }
    public int MaxHealth { get; set; }
    public int Atk { get; set; }
    public int Def {  get; set; }

    public override string ToString()
    {
        return $"{Id} / {StringName} / {StringDesc} / {Level} / {MaxHealth} / {Atk} / {Def} / {Icon}";
    }

    public string ToLocalizedString()
    {
        string formatString = DataTableManager.StringTable.Get(Defines.c_characterStatusTextKey);
        return string.Format(formatString, Level, MaxHealth, Def, Atk);
    }
}

public class CharacterTable : DataTable
{
    Dictionary<string, CharacterData> table = new();

    public override void Load(string fileName)
    {
        table.Clear();

        var path = string.Format(DataTable.FormatPath, fileName);
        var textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCsv<CharacterData>(textAsset.text);

        foreach (CharacterData character in list)
        {
            if (table.ContainsKey(character.Id))
            {
                Debug.LogError($"캐릭터 아이디 중복: {character}");
                continue;
            }

            table[character.Id] = character;
        }
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            return null;
        }

        return table[id];
    }
}
