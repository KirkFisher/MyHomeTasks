using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public GameObject InventoryPrefab;
    public InventoryObject Inventory;

    public int X_START;
    public int Y_START;
    public int NUMBER_OF_COLUMN;
    public int X_SPACE_ITEM;
    public int Y_SPACE_ITEM;
    private Dictionary<GameObject, InventorySlot> ItemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }

    private void Update()
    {
        UpdateSlot();
    }

    public void CreateSlots()
    {
        ItemsDisplayed = new Dictionary<GameObject, InventorySlot> ();
        for(int i = 0; i < Inventory.Container.Items.Length; i++)
        {
            Debug.Log("Creating slot: " + i);
            var obj = Instantiate(InventoryPrefab, Vector2.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);


            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragBegin(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDraged(obj); });

            ItemsDisplayed.Add(obj, Inventory.Container.Items[i]);
        }
    }

    public void UpdateSlot()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in ItemsDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                Debug.Log("Updating Slot - ID: " + _slot.Value.ID + ", Item ID: " + _slot.Value.item.Id);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
                    Inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                //_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    _slot.Value.ammount == 1 ? "" : _slot.Value.ammount.ToString("n0");
            } else
            {
                Debug.Log("Updating Slot - Empty");
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                //_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.green;
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void OnDraged(GameObject obj)
    {
       if(mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    private void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            Debug.Log("OnDragEnd: Moving item");
            Inventory.MoveItem(ItemsDisplayed[obj], ItemsDisplayed[mouseItem.hoverObj]);
        } else
        {
            Debug.Log("OnDragEnd: Removing item");
            Inventory.RemoveItem(ItemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    private void OnDragBegin(GameObject obj)
    {
        Debug.Log("OnDragBegin");
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        Debug.Log(ItemsDisplayed[obj].item.Name);
        if (ItemsDisplayed[obj].ID >= 0)
        {
            Debug.Log("OnDragBegin1");
            var img = mouseObject.AddComponent<Image>();
            img.sprite = Inventory.database.GetItem[ItemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = ItemsDisplayed[obj];
    }

    private void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }

    private void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if (ItemsDisplayed.ContainsKey(obj))
        {
            mouseItem.hoverItem = ItemsDisplayed[obj];
        }
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_ITEM * (i % NUMBER_OF_COLUMN)),
             Y_START+(-Y_SPACE_ITEM * (i / NUMBER_OF_COLUMN)),0);
   
    }
}

public class MouseItem
{
   public GameObject obj;
   public InventorySlot item;
   public InventorySlot hoverItem;
   public GameObject hoverObj;
}
