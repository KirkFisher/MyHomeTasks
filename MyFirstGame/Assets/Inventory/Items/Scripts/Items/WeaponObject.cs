using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Object", menuName = "Inventory System/ Items/ Weapon")]

public class WeaponObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Weapon;
    }
}
