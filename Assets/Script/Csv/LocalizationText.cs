using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LocalizationText : MonoBehaviour
{

    public string id;
    public Defines.Languages editorLanguage;
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
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            OnChangeLanguage();
        }
        else
        {
            OnChangeLanguage(editorLanguage);
        }
#endif
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Variables.OnLanguageChaged += OnChangeLanguage;
        Variables.OnLanguageResetRequested += ResetLanguage;

        if (Application.isPlaying)
        {
            OnChangeLanguage();
        }
        else
        {
            OnChangeLanguage(editorLanguage);
        }
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= OnChangeLanguage;
        Variables.OnLanguageResetRequested -= ResetLanguage;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Defines.Languages.Korean;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Defines.Languages.English;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Defines.Languages.Japanesse;
        }
#endif
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Defines.Languages language)
    {
        var stringTable = DataTableManager.GetStringTable(language);
        text.text = stringTable.Get(id);
    }

    public void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    [ContextMenu("ChangeLanguage")]
    public void ChangeLanguage()
    {
        Variables.Language = editorLanguage;
        Variables.ResetLanguages();
    }

    private void ResetLanguage()
    {
        editorLanguage = Variables.Language;
        OnChangeLanguage();
    }
#endif
}
