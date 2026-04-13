using UnityEngine;

public class DataTableButton:MonoBehaviour
{
    StringTable table;

    private void Start()
    {
        table = new StringTable();
    }


    public void OnButtonClick(string fileName)
    {
        table.Load(fileName);
        foreach(var item in table.table)
        {
            Debug.Log($"{item.Key} : {item.Value}");
        }

        Debug.Log(DataTableManager.StringTable.Get("YOU DIE"));
    }
}
