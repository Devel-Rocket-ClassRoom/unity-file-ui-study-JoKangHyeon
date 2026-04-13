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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.languages = Defines.Languages.Korean;
            text.text = DataTableManager.StringTable.Get(id);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.languages = Defines.Languages.English;
            text.text = DataTableManager.StringTable.Get(id);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.languages = Defines.Languages.Japanesse;
            text.text = DataTableManager.StringTable.Get(id);
        }
    }
}
