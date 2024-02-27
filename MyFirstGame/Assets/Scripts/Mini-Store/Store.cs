using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel; // ѕанель магазина

    private void Start()
    {
        shopPanel.SetActive(false);
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);

        // ≈сли панель магазина была открыта, выключаем ее и возобновл€ем игровую сцену
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
        // ѕриостанавливаем игровое врем€ (или что-то еще в зависимости от ваших потребностей)
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        // ¬озобновл€ем игровое врем€ при закрытии панели магазина
        Time.timeScale = 1f;
    }
}
