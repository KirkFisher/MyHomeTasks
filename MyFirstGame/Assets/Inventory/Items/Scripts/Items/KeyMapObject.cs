using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KeyMap Object", menuName = "Inventory System/ Items/ KeyMap")]

public class KeyMapObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.KeyMap;
    }
}
