using JetBrains.Annotations;
using UnityEngine;

public enum ItemType
{
    Food,
    Equpment,
    Weapon,
    Default,
    KeyMap
}

public enum Attributes
{
    Health,
    Stamina,
    Strenght
}

public class ItemsObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    public int itemPrice;
    [TextArea(10, 10)] public string Description;

    public ItemBuff[] buff;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public int itemPrice;
    public ItemBuff[] buff;

    public Item(ItemsObject item)
    {
        Name = item.name;
        Id = item.Id;
        buff = new ItemBuff[item.buff.Length];
        for (int i = 0; i < buff.Length; i++)
        {
            buff[i] = new ItemBuff(item.buff[i].Min, item.buff[i].Max)
            {
                attributes = item.buff[i].attributes
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attributes;
    public int Value;
    public int Max;
    public int Min;

    public ItemBuff(int min, int max)
    {
        Min = min; Max = max;
        GenerateValue();
    }

    private void GenerateValue()
    {
        Value = Random.Range(Min, Max);
    }
}
