using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public ViewableViewer targetViewer;
    public CharacterViewer buttonViewer;

    public void OnButtonClicked()
    {
        targetViewer.ItemData = buttonViewer.ItemData;
    }
}
