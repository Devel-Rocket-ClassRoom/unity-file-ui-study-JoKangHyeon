using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public CharacterViewer targetViewer;
    public CharacterViewer buttonViewer;

    public void OnButtonClicked()
    {
        targetViewer.ItemData = buttonViewer.ItemData;
    }
}
