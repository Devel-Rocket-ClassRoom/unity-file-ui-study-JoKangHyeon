using System.IO;
using System.Text;
using UnityEngine;

public class Question1 : MonoBehaviour
{
    string c_SaveDataFolder = "SaveData";

    private void Start()
    {
        StringBuilder sbDebug = new StringBuilder();

        string saveDataFolder = Path.Combine(Application.persistentDataPath, c_SaveDataFolder);
        Directory.CreateDirectory(saveDataFolder);

        File.WriteAllText(Path.Combine(saveDataFolder, "save1.txt"), "aaaa");
        File.WriteAllText(Path.Combine(saveDataFolder, "save2.txt"), "bbbb");
        File.WriteAllText(Path.Combine(saveDataFolder, "save3.txt"), "cccc");

        
        sbDebug.AppendLine("=== 세이브 파일 목록 ===");
        foreach (var file in Directory.GetFiles(saveDataFolder))
        {
            sbDebug.AppendLine($"{Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }


        File.Copy(Path.Combine(saveDataFolder, "save1.txt"),
            Path.Combine(saveDataFolder, "save1_backup.txt"));
        sbDebug.AppendLine("save1.txt → save1_backup.txt 복사 완료");


        File.Delete(Path.Combine(saveDataFolder, "save3.txt"));
        sbDebug.AppendLine("save3.txt 삭제 완료");


        sbDebug.AppendLine("=== 작업 후 파일 목록 ===");
        foreach (var file in Directory.GetFiles(saveDataFolder))
        {
            sbDebug.AppendLine($"{Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }


        Debug.Log(sbDebug.ToString());
    }
}
