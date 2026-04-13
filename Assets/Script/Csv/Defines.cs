using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Defines;

public class Defines
{
    public enum Languages
    {
        Korea,
        English,
        Japanesse
    }
}

public static class Variables
{
    public static Languages languages = Languages.Korea;
}

public static class DataTableIds
{
    public static readonly string[] StringTables =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };
    public static string String => StringTables[(int)Variables.languages];
}

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new();

    public static StringTable StringTable => Get<StringTable>(DataTableIds.String);

    static DataTableManager() {
        Init();
    }

    public static void Init()
    {
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
    }

    public static T Get<T> (string id) where T : DataTable
    {
        Debug.Log(id);

        if (!tables.ContainsKey(id))
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }

        return tables[id] as T;
    }
}