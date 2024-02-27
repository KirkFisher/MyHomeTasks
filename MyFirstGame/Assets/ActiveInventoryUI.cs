using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventoryUI : MonoBehaviour
{
    public InventoryObject activeInventory; // ������ �� �������� ���������
    public GameObject slotPrefab; // ������ ����� � ����� UI
    public Transform slotsParent; // ������������ ������ ��� ���� ������

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
            // ���������� ����������� � ���������� �������� � �����
            // ����� �������� ����������� ������� ��� ���������� � ����������
            itemsDisplayed.Add(slotObj, slot);
        }
    }

    // �������� ������ ��� ����������� � ������������� ��������� �� ��������� ���������
    // ��������:

    public void MoveItemToActiveInventory(GameObject obj)
    {
        if (itemsDisplayed.TryGetValue(obj, out InventorySlot slot))
        {
            // ������� ����� ��� ����������� �������� � �������� ���������
            // activeInventory.AddItem(slot.item, slot.ammount);
            // Inventory.RemoveItem(slot.item); // ������� �� ��������� ���������
        }
    }

    public void UseItemFromActiveInventory(GameObject obj)
    {
        if (itemsDisplayed.TryGetValue(obj, out InventorySlot slot))
        {
            // ������� ����� ��� ������������� �������� �� ��������� ���������
            // activeInventory.UseItem(slot, PlayerHealth.Instance);
        }
    }
}