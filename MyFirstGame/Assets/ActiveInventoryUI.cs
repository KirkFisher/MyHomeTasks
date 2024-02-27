using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventoryUI : MonoBehaviour
{
    public InventoryObject activeInventory; // Ссылка на активный инвентарь
    public GameObject slotPrefab; // Префаб слота в вашем UI
    public Transform slotsParent; // Родительский объект для всех слотов

    private Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        foreach (InventorySlot slot in activeInventory.Container.Items)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotsParent);
            // Установить изображение и количество предмета в слоте
            // Также добавить обработчики событий для интеракции с предметами
            itemsDisplayed.Add(slotObj, slot);
        }
    }

    // Добавьте методы для перемещения и использования предметов из активного инвентаря
    // Например:

    public void MoveItemToActiveInventory(GameObject obj)
    {
        if (itemsDisplayed.TryGetValue(obj, out InventorySlot slot))
        {
            // Вызвать метод для перемещения предмета в активный инвентарь
            // activeInventory.AddItem(slot.item, slot.ammount);
            // Inventory.RemoveItem(slot.item); // Удалить из основного инвентаря
        }
    }

    public void UseItemFromActiveInventory(GameObject obj)
    {
        if (itemsDisplayed.TryGetValue(obj, out InventorySlot slot))
        {
            // Вызвать метод для использования предмета из активного инвентаря
            // activeInventory.UseItem(slot, PlayerHealth.Instance);
        }
    }
}