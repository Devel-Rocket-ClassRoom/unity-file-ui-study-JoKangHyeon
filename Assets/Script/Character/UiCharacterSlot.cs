using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiCharacterSlot : MonoBehaviour
{

    public int slot;
    SaveCharacterData _character;
    public SaveCharacterData Character
    {
        get { return _character; }
        set
        {
            _character = value;
            UiRefresh();
        }
    }

    public Button button;
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    private void OnEnable()
    {
        Variables.OnLanguageChaged += UiRefresh;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= UiRefresh;
    }

    public void SetEmpty()
    {
        Character= null;
    }

    public void UiRefresh()
    {
        if (Character == null)
        {
            characterImage.sprite = null;
            nameText.text = string.Empty;
            descriptionText.text = string.Empty;
        }
        else
        {
            characterImage.sprite = Character.CharacterData.SpriteIcon;
            nameText.text = Character.CharacterData.StringName;
            descriptionText.text = Character.CharacterData.StringDesc;
        }
    }
}
