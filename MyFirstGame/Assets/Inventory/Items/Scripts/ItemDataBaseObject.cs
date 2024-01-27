using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDatabase", menuName = "Inventory/Items/Database")]

public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemsObject[] Items;
    public Dictionary<int, ItemsObject> GetItem;

    public void OnAfterDeserialize()
    {
        //Items = new ItemsObject[0];
        GetItem = new Dictionary<int, ItemsObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemsObject>();
    }
}
