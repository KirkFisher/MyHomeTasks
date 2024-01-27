using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Object", menuName = "Inventory System/ Items/ Food")]

public class FoodObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Food;
    }
}
