using UnityEngine;
using UnityEngine.UI;

/*public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button buyButton;
    [SerializeField] private int itemCost;
    [SerializeField] private GameObject npcTrader; // НПС торговец
    // Ссылка на инвентарь игрока
    public PlayerInventory playerInventory;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        // Проверяем, достаточно ли монет у игрока для покупки
        if (playerInventory.GetCoins() >= itemCost)
        {
            // Отнимаем стоимость от монет игрока
            playerInventory.SetCoins(playerInventory.GetCoins() - itemCost);
            // Проверяем, есть ли ссылка на торговца
            if (npcTrader != null)
            {
                // Создаем объект на сцене рядом с торговцем
                Instantiate(itemPrefab, npcTrader.transform.position + Vector3.right, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Ссылка на торговца отсутствует!");
            }
        }
        else
        {
            Debug.Log("Недостаточно средств для покупки!");
        }
    }

    // Метод для установки ссылки на торговца
    public void SetNpcTrader(GameObject npc)
    {
        npcTrader = npc;
    }
}*/
public class Slot : MonoBehaviour
{
    [SerializeField] private ItemsObject item; // Скриптейбл нужного объекта
    [SerializeField] private Button buyButton;
    [SerializeField] private int itemCost;

    // Ссылка на инвентарь игрока
    public InventoryObject playerInventory;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        // Проверяем, достаточно ли монет у игрока для покупки
        if (playerInventory.GetCoins() >= itemCost)
        {
            // Уменьшаем количество монет у игрока
            playerInventory.SetCoins(playerInventory.GetCoins() - itemCost);

            // Создаем новый предмет и добавляем его в инвентарь игрока
            Item newItem = item.CreateItem(); // Создаем предмет из скриптейбла
            playerInventory.AddItem(newItem, 1);

            // Очищаем ссылку на торговца, так как покупка завершена
            SetNpcTrader(null);
        }
        else
        {
            Debug.Log("Недостаточно средств для покупки!");
        }
    }

    // Метод для установки ссылки на торговца (если необходимо)
    public void SetNpcTrader(GameObject npc)
    {
        // Не используется в данном случае, но может быть полезно для дальнейшего расширения
    }
}
