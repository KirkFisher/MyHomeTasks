using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Object", menuName = "Inventory System/ Items/ Equpment")]

public class EqupmentObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Equpment;
    }
}
