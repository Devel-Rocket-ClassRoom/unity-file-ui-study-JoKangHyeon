using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventorySlot : MonoBehaviour
{
    public int slotIndex;
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public SaveItemData SaveItemData { get; protected set; }
    
    public Button button;

    private void OnEnable()
    {
        Variables.OnLanguageChaged += RefreshText;
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= RefreshText;
    }

    public void SetEmpty()
    {
        SaveItemData = null;
        itemImage.sprite = null;
        itemNameText.text = string.Empty;
    }

    public void SetItem(SaveItemData item)
    {
        SaveItemData = item;
        itemImage.sprite = item.ItemData.SpriteIcon;
        itemNameText.text = item.ItemData.StringName;
    }

    private void RefreshText()
    {
        if (SaveItemData != null)
        {
            itemNameText.text = SaveItemData.ItemData.StringName;
        }
    }
}
