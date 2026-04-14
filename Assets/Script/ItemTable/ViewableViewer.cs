using UnityEngine;
using UnityEngine.UI;

public class ViewableViewer : MonoBehaviour
{
    public LocalizationText nameText;
    public LocalizationText descriptionText;
    public Image icon;

    public string itemId;

    ViewableData _data;

    public ViewableData ItemData
    {
        get
        {
            return _data;
        }

        set
        {
            _data = value;
            if (_data == null)
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
                nameText.id = _data.Name;
                nameText.OnChangeLanguage();
            }

            if (descriptionText != null)
            {
                descriptionText.id = _data.Desc;
                descriptionText.OnChangeLanguage();
            }

            if (icon != null)
            {
                icon.sprite = _data.SpriteIcon;
            }
        }
    }
}
