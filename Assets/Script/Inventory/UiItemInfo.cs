using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0} : {1}";

    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    SaveItemData item;

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
        item = null;
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }

    public void SetItem(SaveItemData item)
    {
        this.item = item;
        imageIcon.sprite = item.ItemData.SpriteIcon;
        textName.text = string.Format(FormatCommon,DataTableManager.StringTable.Get("NAME"), item.ItemData.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("DESC"), item.ItemData.StringDesc);
        textType.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("TYPE"), DataTableManager.StringTable.Get(item.ItemData.Type.ToString()));
        textValue.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("VALUE"), item.ItemData.Value.ToString());
        textCost.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("COST"), item.ItemData.Cost.ToString());
    }

    public void RefreshText()
    {
        if (item != null)
        {
            SetItem(item);
        }
    }
}
