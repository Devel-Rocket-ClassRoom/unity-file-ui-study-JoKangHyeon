using System.Linq;
using System.Text;
using UnityEngine;
using SaveDataVC = SaveDataV3;

public class SaveLoadTest1 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Save();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SaveLoadManager.Load();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name : {SaveLoadManager.Data.Name}");
            sb.AppendLine($"Gold : {SaveLoadManager.Data.Gold}");

            sb.AppendLine("items : ");
            foreach (var item in SaveLoadManager.Data.items)
            {
                var itemData = DataTableManager.ItemTable.Get(item);
                sb.AppendLine($"    {item} : { DataTableManager.StringTable.Get(itemData.Name)}");
            }
            Debug.Log( sb.ToString() );
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int randomItem = Random.Range(0, DataTableManager.ItemTable.table.Count);
            string itemId = DataTableManager.ItemTable.table.Keys.ToList()[randomItem];
            SaveLoadManager.Data.items.Add(itemId);

            Debug.Log($"Added : {itemId}");
        }
    }
}
