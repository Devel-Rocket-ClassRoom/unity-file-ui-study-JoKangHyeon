using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string UiTextFormat = "{0} : {1}";

    public Image characterSprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;

    public Image weaponImage;
    public Image equipImage;

    public Button weaponButton;
    public Button equipButton;

    [Header("Data")]
    public Sprite itemEmptySprite;

    [Header("Another Panel")]
    public UiPanelInventory inventory;
    public UiCharacterPanel characterPanel;

    private bool weaponOrEquip = false;
    private SaveCharacterData _saveCharacterData;
    public SaveCharacterData SaveCharacterData
    {
        get { return _saveCharacterData; }
        set
        {
            _saveCharacterData = value;
            UiRefresh();
        }
    }

    private void OnEnable()
    {
        Variables.OnLanguageChaged += UiRefresh;
        equipButton.onClick.AddListener(OnEquipButton);
        weaponButton.onClick.AddListener(OnWeaponButton);

        UiRefresh();
    }

    private void OnDisable()
    {
        Variables.OnLanguageChaged -= UiRefresh;
    }


    public void OnWeaponButton()
    {
        inventory.inventorySlotList.onSelectSlot.RemoveAllListeners();
        inventory.gameObject.SetActive(true);
        inventory.inventorySlotList.Filtering = UiInventorySlotList.FilteringOptions.Weapon;
        inventory.inventorySlotList.onSelectSlot.AddListener(OnWeaponSelected);
        weaponOrEquip = true;
    }

    public void OnWeaponSelected(SaveItemData item)
    {
        if (SaveCharacterData.Equipments[0] != null)
        {
            inventory.inventorySlotList.saveItemDataList.Add(SaveCharacterData.Equipments[0]);
        }

        SaveCharacterData.Equipments[0] = item;
        inventory.inventorySlotList.saveItemDataList.Remove(item);
        FullSave();
        CloseWeaponSelectPanel();
    }

    public void CloseWeaponSelectPanel()
    {
        inventory.inventorySlotList.onSelectSlot.RemoveAllListeners();
        inventory.gameObject.SetActive(false);
        UiRefresh();
    }



    public void OnEquipButton()
    {
        inventory.inventorySlotList.onSelectSlot.RemoveAllListeners();
        inventory.gameObject.SetActive(true);
        inventory.inventorySlotList.Filtering = UiInventorySlotList.FilteringOptions.Equip;
        inventory.inventorySlotList.onSelectSlot.AddListener(OnEquipSelected);
        weaponOrEquip = false;
    }

    public void OnEquipSelected(SaveItemData item)
    {
        if (SaveCharacterData.Equipments[1] != null)
        {
            inventory.inventorySlotList.saveItemDataList.Add(SaveCharacterData.Equipments[0]);
        }

        SaveCharacterData.Equipments[1] = item;
        inventory.inventorySlotList.saveItemDataList.Remove(item);
        FullSave();
        CloseEquipSelectPanel();
    }

    public void CloseEquipSelectPanel()
    {
        inventory.inventorySlotList.onSelectSlot.RemoveAllListeners();
        inventory.gameObject.SetActive(false);
        UiRefresh();
    }

    public void OnCancelSelected()
    {
        if(weaponOrEquip)
        {
            CloseWeaponSelectPanel();
        }
        else
        {
            CloseEquipSelectPanel();
        }
    }

    public void OnRemoveSelected()
    {
        if (weaponOrEquip)
        {
            if (SaveCharacterData.Equipments[0] != null)
            {
                inventory.inventorySlotList.saveItemDataList.Add(SaveCharacterData.Equipments[0]);
            }
            SaveCharacterData.Equipments[0] = null;
            CloseWeaponSelectPanel();
        }
        else
        {
            if (SaveCharacterData.Equipments[1] != null)
            {
                inventory.inventorySlotList.saveItemDataList.Add(SaveCharacterData.Equipments[0]);
            }
            SaveCharacterData.Equipments[1] = null;
            FullSave();
            CloseEquipSelectPanel();
        }
    }

    public void UiRefresh()
    {
        if(SaveCharacterData == null)
        {
            characterSprite.sprite = itemEmptySprite;
            nameText.text = string.Empty;
            descriptionText.text = string.Empty;
            levelText.text = string.Empty;
            hpText.text = string.Empty;
            atkText.text = string.Empty;
            defText.text = string.Empty;
            weaponImage.sprite = itemEmptySprite;
            equipImage.sprite = itemEmptySprite;
            return;
        }


        characterSprite.sprite = SaveCharacterData.CharacterData.SpriteIcon;
        nameText.text = string.Format(UiTextFormat, 
            DataTableManager.StringTable.Get("NAME"), SaveCharacterData.CharacterData.StringName);
        descriptionText.text = string.Format(UiTextFormat, 
            DataTableManager.StringTable.Get("DESC"), SaveCharacterData.CharacterData.StringDesc);

        levelText.text = string.Format(UiTextFormat,
            DataTableManager.StringTable.Get("LEVEL"), SaveCharacterData.CharacterData.Level);
        hpText.text = string.Format(UiTextFormat,
            DataTableManager.StringTable.Get("HP"), SaveCharacterData.CharacterData.MaxHealth);


        int atk = SaveCharacterData.CharacterData.Atk;
        if (SaveCharacterData.Equipments[0] != null)
        {
            atk += SaveCharacterData.Equipments[0].ItemData.Value;
            weaponImage.sprite = SaveCharacterData.Equipments[0].ItemData.SpriteIcon;
        }
        else
        {
            weaponImage.sprite = itemEmptySprite;
        }
        atkText.text = string.Format(UiTextFormat,
            DataTableManager.StringTable.Get("ATK"), atk);

        int def = SaveCharacterData.CharacterData.Def;
        if (SaveCharacterData.Equipments[1] != null)
        {
            def += SaveCharacterData.Equipments[1].ItemData.Value;
            equipImage.sprite = SaveCharacterData.Equipments[1].ItemData.SpriteIcon;
        }
        else
        {
            equipImage.sprite = itemEmptySprite;
        }
        defText.text = string.Format(UiTextFormat,
            DataTableManager.StringTable.Get("DEF"), def);

    }

    public void FullSave()
    {
        SaveLoadManager.Data.characters = characterPanel.characterList.saveCharacterDatas;
        SaveLoadManager.Data.items = inventory.inventorySlotList.saveItemDataList;
        SaveLoadManager.Save();
        UiRefresh();
    }
}
