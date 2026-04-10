using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Question3 : MonoBehaviour
{
    Dictionary<string, string> config;

    private void Start()
    {
        StringBuilder sbDebug = new StringBuilder();

        GetDefaultConfig();
        Save();

        config = null;
        Load();

        sbDebug.AppendLine($"설정 로드 완료 (항목 {config.Count}개)");

        
        sbDebug.AppendLine("--- 변경 전 ---");
        sbDebug.AppendLine($"bgm_volume = {config["bgm_volume"]}");
        sbDebug.AppendLine($"language = {config["language"]}");

        
        config["bgm_volume"] = "50";
        config["language"] = "en";
        Save();
        sbDebug.AppendLine("--- 변경 후 저장 ---");
        sbDebug.AppendLine($"bgm_volume = {config["bgm_volume"]}");
        sbDebug.AppendLine($"language = {config["language"]}");


        sbDebug.AppendLine("--- 최종 파일 내용 ---");
        sbDebug.AppendLine(File.ReadAllText(Path.Combine(Application.persistentDataPath, "settings.cfg")));

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
    }
}
