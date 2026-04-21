
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public List<string> optionKeys;

    private void Reset()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
    }

    void OnEnable()
    {
        Variables.OnLanguageChaged += OnChangeLanguage;
        OnChangeLanguage();
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= OnChangeLanguage;
    }

    private void OnChangeLanguage()
    {
        for(int i=dropdown.options.Count; i< optionKeys.Count; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData());
        }

        for(int i=0; i< optionKeys.Count; i++)
        {
            string text = DataTableManager.StringTable.Get(optionKeys[i]);
            if (text != null)
            {
                dropdown.options[i].text = text;
            }
        }

        for(int i= optionKeys.Count; i< dropdown.options.Count; )
        {
            dropdown.options.RemoveAt(i);
        }

        dropdown.RefreshShownValue();
    }
}
