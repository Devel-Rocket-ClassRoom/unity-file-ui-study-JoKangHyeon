using UnityEngine;
using static UiCharacterList;

public class UiCharacterPanel : MonoBehaviour
{
    public UiCharacterList characterList;
    public UiCharacterInfo characterInfo;

    private void OnEnable()
    {
        characterList.Load();
        characterList.onSelectSlot.AddListener(OnSelectSlot);
    }

    private void OnDisable()
    {
        characterList.onSelectSlot.RemoveListener(OnSelectSlot);
    }

    public void OnFilteringChaged(int index)
    {
        characterList.Filter = (Filterings)index;
    }

    public void OnSortingChaged(int index)
    {
        characterList.Sorting = (Sortings)index;
    }
    public void OnSave()
    {
        characterList.Save();
    }

    public void OnLoad()
    {
        characterList.Load();
    }

    public void OnAddRandomCharacter()
    {
        characterList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        characterList.RemoveCharacter();
    }

    public void OnSelectSlot(SaveCharacterData characterData)
    {
        characterInfo.SaveCharacterData = characterData;
    }

}
