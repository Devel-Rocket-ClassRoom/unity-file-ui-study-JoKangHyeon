using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class Question3 : MonoBehaviour
{
    Dictionary<string, string> config;

    public TextMeshProUGUI dataText;

    private void Start()
    {
        StringBuilder sbDebug = new StringBuilder();

        GetDefaultConfig();
        Save();

        config = null;
        Load();

        sbDebug.AppendLine($"설정 로드 완료 (항목 {config.Count}개)");

        Debug.Log(sbDebug.ToString());
    }

    public void GetDefaultConfig()
    {
        config = new Dictionary<string, string>
        {
            { "master_volume", "80" },
            { "bgm_volume", "70" },
            { "sfx_volume", "90" },
            { "language", "kr" },
            { "show_damage", "true" }
        };
        Debug.Log("설정 복원 완료");
    }

    public void ChangeConfig()
    {
        config["bgm_volume"] = "50";
        config["language"] = "en";
        Debug.Log("설정 변경 완료");
    }

    public void SetUI()
    {
        try
        {
        dataText.text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "settings.cfg"));

        }
        catch
        {
            dataText.text = "";
        }
    }

    public void Save()
    {
        using(FileStream fs = File.OpenWrite(Path.Combine(Application.persistentDataPath,"settings.cfg")))
        using(StreamWriter sw = new StreamWriter(fs))
        {
            foreach(KeyValuePair<string, string> kvp in config)
            {
                sw.WriteLine($"{kvp.Key}={kvp.Value}");
            }
        }
        SetUI();
    }

    public void Load()
    {
        config = new();

        using (FileStream fs = File.OpenRead(Path.Combine(Application.persistentDataPath, "settings.cfg")))
        using (StreamReader sr = new StreamReader(fs))
        {
            string read = sr.ReadLine();
            while (read != null)
            {
                string[] splited = read.Split('=');
                config.Add(splited[0], splited[1]);
                read = sr.ReadLine();
            }
        }
        SetUI();
    }
}
