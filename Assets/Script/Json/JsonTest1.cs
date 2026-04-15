using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class PlayerStatus
{
    public string playerName;
    public int lives;
    public float health;

    //[JsonConverter(typeof(Vector3Converter))]
    public Vector3 pos;

    public override string ToString()
    {
        return $"{playerName} {lives} {health} {pos}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var obj = new PlayerStatus()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                pos = new Vector3(1f, 2f, 3f)
            };

            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");
            string json = JsonConvert.SerializeObject(obj, jsonSetting);

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
            PlayerStatus obj = JsonConvert.DeserializeObject<PlayerStatus>(json, jsonSetting);
            Debug.Log($"{obj}");
        }
    }
}
