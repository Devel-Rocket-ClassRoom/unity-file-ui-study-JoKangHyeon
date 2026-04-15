using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;


public class TransformData
{
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 pos;
    [JsonConverter(typeof(QuaternionConverter))]
    public Quaternion rot;
    [JsonConverter(typeof(Vector3Converter))]
    public Vector3 scale;
    [JsonConverter(typeof(ColorConverter))]
    public Color color;

    public int type;

    public override string ToString()
    {
        return $"pos:{pos} / rot:{rot} / scale:{scale} / color:{color}";
    }
}


public class JsonTest2 : MonoBehaviour
{
    public List<GameObject> prefabs;

    public Vector2 createCountRange;
    public Vector2 posXRange;
    public Vector2 posYRange;
    public Vector2 scaleRange;

    List<GameObject> objects;


    private void Awake()
    {
        objects = new();
    }

    public void Create()
    {
        int count = Random.Range((int)createCountRange.x, (int)createCountRange.y);

        for(int i=0; i < count; i++)
        {
            int type = Random.Range(0, prefabs.Count);
            GameObject go = Instantiate(prefabs[type]);

            Vector2 pos = new Vector3(Random.Range(posXRange.x, posXRange.y), Random.Range(posYRange.x, posYRange.y));
            float scale = Random.Range(scaleRange.x, scaleRange.y);
            Vector3 scaleVec = new Vector3(scale,scale,scale);
            Quaternion rot = Random.rotation;
            Color color = Random.ColorHSV();

            go.transform.position = pos;
            go.transform.rotation = rot;
            go.transform.localScale = scaleVec;
            go.GetComponent<Renderer>().material.color = color;
            go.name = type.ToString();
            objects.Add(go);
        }
    }

    public void Clear()
    {
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }

        objects.Clear();
    }

    public void Save()
    {
        List<TransformData> data = new();

        foreach(GameObject saveTarget in objects)
        {
            var obj = new TransformData()
            {
                pos = saveTarget.transform.position,
                rot = saveTarget.transform.rotation,
                scale = saveTarget.transform.localScale,
                color = saveTarget.GetComponent<Renderer>().material.color,
                type = int.Parse(saveTarget.name)
            };
            data.Add(obj);
        }

        string path = Path.Combine(Application.persistentDataPath, "JsonTest", "many.json");
        
        string json = JsonConvert.SerializeObject(data);

        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "JsonTest")))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "JsonTest"));
        }

        File.WriteAllText(path, json);
        Debug.Log(path);
    }

    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "JsonTest", "many.json");
        string json = File.ReadAllText(path);
        
        List<TransformData> obj = JsonConvert.DeserializeObject<List<TransformData>>(json);
        Debug.Log($"{obj}");

        Clear();

        foreach(var data in obj)
        {
            GameObject go = Instantiate(prefabs[data.type]);
            go.transform.position = data.pos;
            go.transform.rotation = data.rot;
            go.transform.localScale = data.scale;
            go.GetComponent<Renderer>().material.color = data.color;
            go.name = data.type.ToString();
            objects.Add(go);
        }
    }
}
