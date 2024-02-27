using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory System/Items/Potion")]
public class PotionObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Potion;
    }

    public void Use(PlayerHealth player)
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
