using UnityEngine;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDataBaseObject database;
    public Inventory Container;

    public void AddItem(Item item, int ammount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == item.Id)
            {
                Container.Items[i].AddAmount(ammount);
                return;
            }
        }
        SetEmptySlot(item, ammount);
    }

    public void RemoveItem(Item item)
    {
        int index = -1;
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == item)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            if (Container.Items[index].ammount > 1)
                Container.Items[index].AddAmount(-1);
            else
                Container.Items[index].UpdateSlot(-1, null, 0);
        }
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.ammount);
        item2.UpdateSlot(item1.ID, item1.item, item1.ammount);
        item1.UpdateSlot(temp.ID, temp.item, temp.ammount);
    }

    
    // Метод для получения количества монет
    public int GetCoins()
    {
        return CoinsText.Coins;
    }

    // Метод для установки количества монет
    public void SetCoins(int amount)
    {
        CoinsText.Coins = amount;
    }

    public InventorySlot SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(item.Id, item, amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    public void UsePotion(PlayerHealth player, PotionObject potion)
    {
        potion.Use(player);
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[10];
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    public int ammount;

    public InventorySlot()
    {
        ID = -1;
        item = null;
        ammount = 0;
    }
    public InventorySlot(int _id, Item _item, int _ammount)
    {
        ID = _id;
        item = _item;
        ammount = _ammount;
    }

    public void UpdateSlot(int _id, Item _item, int _ammount)
    {
        ID = _id;
        item = _item;
        ammount = _ammount;
    }

    public void AddAmount(int value)
    {
        ammount += value;
    }
}
