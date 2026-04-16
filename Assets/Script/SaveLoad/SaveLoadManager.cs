using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using SaveDataVC = SaveDataV3;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,
        Encrypted
    }

    public static SaveMode Mode { get; set; } = SaveMode.Encrypted;

    public static int SaveDataVersion { get; } = 3;
    public static readonly string SaveDirectory = Path.Combine($"{Application.persistentDataPath}", "Save");

    private static readonly string[] TextSaveFileNames =
    {
        "SaveAuto.json",
        "Save1.json",
        "Save2.json",
        "Save3.json",
    };

    private static readonly string[] EncryptedSaveFileNames =
    {
        "SaveAuto.dat",
        "Save1.dat",
        "Save2.dat",
        "Save3.dat",
    };


    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= TextSaveFileNames.Length)
        {
            return false;
        }

        string path = string.Empty;
        switch (Mode)
        {
            case SaveMode.Text:
                path = Path.Combine(SaveDirectory, TextSaveFileNames[slot]);
                break;
            case SaveMode.Encrypted:
                path = Path.Combine(SaveDirectory, EncryptedSaveFileNames[slot]);
                break;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            Debug.Log(path);
            string json = JsonConvert.SerializeObject(Data, settings);
            switch (Mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    byte[] encryptedText = CryptoUtil.Encrypt(json);
                    File.WriteAllBytes(path, encryptedText);
                    break;
            }


            return true;
        }
        catch
        {
            Debug.LogError("SAVE 예외");
            return false;
        }
    }

    public static bool Load(int slot = 0)
    {
        string path = string.Empty;
        switch (Mode)
        {
            case SaveMode.Text:
                path = Path.Combine(SaveDirectory, TextSaveFileNames[slot]);
                break;
            case SaveMode.Encrypted:
                path = Path.Combine(SaveDirectory, EncryptedSaveFileNames[slot]);
                break;
        }

        if (slot < 0 || slot >= TextSaveFileNames.Length)
        {
            return false;
        }
        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            string json = string.Empty;
            switch (Mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    byte[] encryptedText = File.ReadAllBytes(path);
                    json = CryptoUtil.Decrypt(encryptedText);
                    break;
            }

            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

            while (saveData.Version < SaveDataVersion)
            {
                saveData = saveData.VersionUp();
                Debug.Log($"VersionUp to {saveData.Version}");
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.LogError("Load 예외");
            return false;
        }

    }
}