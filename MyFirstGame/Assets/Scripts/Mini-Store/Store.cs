using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel; // ������ ��������

    private void Start()
    {
        shopPanel.SetActive(false);
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);

        // ���� ������ �������� ���� �������, ��������� �� � ������������ ������� �����
        if (!shopPanel.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        // ���������������� ������� ����� (��� ���-�� ��� � ����������� �� ����� ������������)
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        // ������������ ������� ����� ��� �������� ������ ��������
        Time.timeScale = 1f;
    }
}
