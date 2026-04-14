using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    public LocalizationText nameText;
    public LocalizationText descriptionText;
    public Image icon;

    public string itemId;

    ItemData _itemData;
    public ItemData ItemData
    {
        get
        {
            return _itemData;
        }

        set
        {
            _itemData = value;
            if (_itemData == null)
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
                nameText.id = _itemData.Name;
                nameText.OnChangeLanguage();
            }

            if (descriptionText != null)
            {
                descriptionText.id = _itemData.Desc;
                descriptionText.OnChangeLanguage();
            }

            if(icon != null)
            {
                icon.sprite = _itemData.SpriteIcon;
            }
        }
    }

    private void Start()
    {
        ItemData = DataTableManager.ItemTable.Get(itemId);
    }

    private void OnValidate()
    {
        if(!Application.isPlaying) 
            ItemData = DataTableManager.ItemTable.Get(itemId);
    }
}
