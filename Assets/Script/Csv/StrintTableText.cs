using TMPro;
using UnityEngine;

public class StrintTableText : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI text;


    private void Start()
    {
        OnChageId();
    }

    private void OnChageId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
}