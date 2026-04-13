using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour
{

    public string id;
    public Defines.Languages language;
    public TextMeshProUGUI text;


    private void Reset()
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }

    void OnValidate()
    {
        if (!Application.isPlaying)
        {    
            text.text = DataTableManager.Get<StringTable>(DataTableIds.StringTables[(int)language]).Get(id);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
