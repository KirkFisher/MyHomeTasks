using JetBrains.Annotations;
using UnityEngine;

public enum ItemType
{
    Food,
    Equpment,
    Weapon,
    Default,
    KeyMap,
    Potion
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
    public ItemBuff[] buff;
    public ItemType type;

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
        type = item.type;
    }

    public virtual void Use(PlayerHealth player)
    {
        if (type == ItemType.Potion && player != null)
        {
            if (buff.Length > 0)
                player.ApplyBuff(buff[0]);
            else
                Debug.LogWarning("No buffs found for potion: " + Name);
        }
        else
        {
            Debug.LogWarning("This item cannot be used in the current context.");
        }
    }
}

[System.Serializable]
public class Potion : Item
{
    public Potion(ItemsObject item) : base(item) { }

    public override void Use(PlayerHealth player)
    {
        foreach (var buff in buff)
            ApplyBuff(player, buff);
    }

    private void ApplyBuff(PlayerHealth player, ItemBuff buff)
    {
        switch (buff.attributes)
        {
            case Attributes.Health:
                player.Heal(buff.Value);
                break;
            case Attributes.Stamina:
                // Применение бонуса к стамине игрока
                break;
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
