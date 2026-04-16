using System;
using System.Collections.Generic;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

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
        throw new NotImplementedException();
    }
}
