using UnityEngine;
using UnityEngine.UI;

/*public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Button buyButton;
    [SerializeField] private int itemCost;
    [SerializeField] private GameObject npcTrader; // ��� ��������
    // ������ �� ��������� ������
    public PlayerInventory playerInventory;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        // ���������, ���������� �� ����� � ������ ��� �������
        if (playerInventory.GetCoins() >= itemCost)
        {
            // �������� ��������� �� ����� ������
            playerInventory.SetCoins(playerInventory.GetCoins() - itemCost);
            // ���������, ���� �� ������ �� ��������
            if (npcTrader != null)
            {
                // ������� ������ �� ����� ����� � ���������
                Instantiate(itemPrefab, npcTrader.transform.position + Vector3.right, Quaternion.identity);
            }
            else
            {
                Debug.LogError("������ �� �������� �����������!");
            }
        }
        else
        {
            Debug.Log("������������ ������� ��� �������!");
        }
    }

    // ����� ��� ��������� ������ �� ��������
    public void SetNpcTrader(GameObject npc)
    {
        npcTrader = npc;
    }
}*/
public class Slot : MonoBehaviour
{
    [SerializeField] private ItemsObject item; // ���������� ������� �������
    [SerializeField] private Button buyButton;
    [SerializeField] private int itemCost;

    // ������ �� ��������� ������
    public InventoryObject playerInventory;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    private void BuyItem()
    {
        // ���������, ���������� �� ����� � ������ ��� �������
        if (playerInventory.GetCoins() >= itemCost)
        {
            // ��������� ���������� ����� � ������
            playerInventory.SetCoins(playerInventory.GetCoins() - itemCost);

            // ������� ����� ������� � ��������� ��� � ��������� ������
            Item newItem = item.CreateItem(); // ������� ������� �� �����������
            playerInventory.AddItem(newItem, 1);

            // ������� ������ �� ��������, ��� ��� ������� ���������
            SetNpcTrader(null);
        }
        else
        {
            Debug.Log("������������ ������� ��� �������!");
        }
    }

    // ����� ��� ��������� ������ �� �������� (���� ����������)
    public void SetNpcTrader(GameObject npc)
    {
        // �� ������������ � ������ ������, �� ����� ���� ������� ��� ����������� ����������
    }
}
