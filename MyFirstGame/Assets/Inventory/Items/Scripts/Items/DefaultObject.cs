using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Object", menuName = "Inventory System/ Items/ Default")]

public class DefaultObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Default;
    }
}
