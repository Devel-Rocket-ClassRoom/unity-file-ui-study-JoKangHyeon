using System.Diagnostics;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class Question1 : MonoBehaviour
{
    string c_SaveDataFolder = "SaveData";

    public TextMeshProUGUI output;

    string saveDataFolder;

    private void Start()
    {
        saveDataFolder = Path.Combine(Application.persistentDataPath, c_SaveDataFolder);

        Directory.CreateDirectory(saveDataFolder);
        output.text = "폴더 생성 완료";
    }

    public void ReadFileList()
    {
        StringBuilder sbDebug = new StringBuilder();

        sbDebug.AppendLine("=== 파일 목록 ===");
        foreach (var file in Directory.GetFiles(saveDataFolder))
        {
            sbDebug.AppendLine($"{Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }
        output.text = sbDebug.ToString();
    }

    public void CreateDefaultFiles()
    {
        File.WriteAllText(Path.Combine(saveDataFolder, "save1.txt"), "aaaa");
        File.WriteAllText(Path.Combine(saveDataFolder, "save2.txt"), "bbbb");
        File.WriteAllText(Path.Combine(saveDataFolder, "save3.txt"), "cccc");
        output.text = "파일 생성 완료";
    }

    public void CopyFile()
    {
        File.Copy(Path.Combine(saveDataFolder, "save1.txt"),
                Path.Combine(saveDataFolder, "save1_backup.txt"));
        output.text = "save1.txt → save1_backup.txt 복사 완료\"";
    }

    public void DeleteFile()
    {
        File.Delete(Path.Combine(saveDataFolder, "save3.txt"));
        output.text = "save3.txt 삭제 완료";
    }
}
