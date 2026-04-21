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
                sb.AppendLine($"    {item.ItemData.Name} : { item.ItemData }");
            }
            Debug.Log( sb.ToString() );
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int randomItem = Random.Range(0, DataTableManager.ItemTable.table.Count);
            var item = DataTableManager.ItemTable.table.Values.ToList()[randomItem];
            var itemSaveData = new SaveItemData();
            itemSaveData.ItemData = item;
            SaveLoadManager.Data.items.Add(itemSaveData);

            Debug.Log($"Added : {itemSaveData.ItemData}");
        }
    }
}
