using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ViewableViewer targetViewer;
    public ItemViewer buttonViewer;

    public void OnButtonClicked()
    {
        targetViewer.ItemData = buttonViewer.ItemData;
    }
}
