using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterViewer : MonoBehaviour
{
    public LocalizationText nameText;
    public LocalizationText descriptionText;
    public TextMeshProUGUI dataText;
    public Image icon;

    public string itemId;

    CharacterData _characterData;
    public CharacterData ItemData
    {
        get
        {
            return _characterData;
        }

        set
        {
            _characterData = value;
            OnLanguageChanged();

            if (_characterData == null)
            {
                if (nameText != null)
                {
                    nameText.id = string.Empty;
                    nameText.text.text = string.Empty;
                }

                if (descriptionText != null)
                {
                    descriptionText.id = string.Empty;
                    descriptionText.text.text = string.Empty;
                }

                if (icon != null)
                {
                    icon.sprite = null;
                }

                return;
            }

            if (nameText != null)
            {
                nameText.id = _characterData.Name;
                nameText.OnChangeLanguage();
            }

            if (descriptionText != null)
            {
                descriptionText.id = _characterData.Desc;
                descriptionText.OnChangeLanguage();
            }

            if(icon != null)
            {
                icon.sprite = _characterData.SpriteIcon;
            }
        }
    }

    private void Start()
    {
        ItemData = DataTableManager.CharacterTable.Get(itemId);
    }

    private void OnValidate()
    {
        if(!Application.isPlaying) 
            ItemData = DataTableManager.CharacterTable.Get(itemId);
    }

    private void OnEnable()
    {
        Variables.OnLanguageChaged += OnLanguageChanged;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        if (dataText != null)
        {
            dataText.text = _characterData.ToLocalizedString();
        }
    }
}
