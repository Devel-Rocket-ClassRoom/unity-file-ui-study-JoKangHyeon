using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public Dictionary<string, int> scores = new()
    {
        {"stage1",100 },
        {"stage2",200 },
        {"stage3",300 },
    };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var obj = new PlayerInfo()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            string json = JsonUtility.ToJson(obj, true);

            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JsonTest")))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JsonTest"));
            }

            File.WriteAllText(path, json);
            Debug.Log(path);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            string json = File.ReadAllText(path);
            PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);
            Debug.Log($"{obj.playerName} {obj.lives} {obj.health} {obj.position}");
        }
    }
}
