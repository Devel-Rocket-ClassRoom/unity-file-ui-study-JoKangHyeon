using System;
using System.Collections.Generic;
using static Defines;

public class Defines
{
    public enum Languages
    {
        Korean,
        English,
        Japanesse
    }

    public enum ItemTypes
    {
        Weapon,
        Equip,
        Consumable
    }

    public const string c_characterStatusTextKey = "CharacterStatusFormat";
}

public static class Variables
{
    public static event Action OnLanguageChaged;
    public static event Action OnLanguageResetRequested;

    private static Languages _language = Languages.Korean;

    public static Languages Language
    {
        get
        {
            return _language;
        }
        set
        {
            if (_language == value) return;

            DataTableManager.SetStringTable(value);
            _language = value;
            OnLanguageChaged?.Invoke();
        }
    }

    public static void ResetLanguages()
    {
        Variables.OnLanguageResetRequested?.Invoke();
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTables =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };
    public static string String => StringTables[(int)Variables.Language];

    public static readonly string Item = "ItemTable";
    public static readonly string Character = "CharacterTable";
}

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);
    public static ItemTable ItemTable => Get<ItemTable>(DataTableIds.Item);
    public static CharacterTable CharacterTable => Get<CharacterTable>(DataTableIds.Character);

    static DataTableManager()
    {
        Init();
    }

    public static void Init()
    {

#if UNITY_EDITOR
        foreach (string language in DataTableIds.StringTables)
        {
            var stringTable = new StringTable();
            stringTable.Load(language);
            tables.Add(language, stringTable);
        }
#else
        SetStringTable();
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DataTableIds.Item);
        tables.Add(DataTableIds.Item, itemTable);

        var characterTable = new CharacterTable();
        characterTable.Load(DataTableIds.Character);
        tables.Add(DataTableIds.Character, characterTable);
    }

#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DataTableIds.StringTables[(int)lang]);
    }
#endif

    public static T Get<T>(string id) where T : DataTable
    {
        //Debug.Log(id);

        if (!tables.ContainsKey(id))
        {
            return null;
        }

        return tables[id] as T;
    }

    public static void SetStringTable(Languages to)
    {
#if !UNITY_EDITOR
        var stringTable = StringTable;
        stringTable.Load(DataTableIds.StringTables[(int)to]);

        tables.Clear();
        tables.Add(DataTableIds.StringTables[(int)to], stringTable);
#endif
    }
}
