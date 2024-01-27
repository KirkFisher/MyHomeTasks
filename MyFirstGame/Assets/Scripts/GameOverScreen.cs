using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartHandler()
    {
        Scene scene = SceneManager.GetActiveScene(); //перезапуск активной гейм сцены
        SceneManager.LoadScene(scene.name);
        CoinsText.Coins = 0;
        Time.timeScale = 1.0f;
    }

    public void ExitHandler()
    {
        SceneManager.LoadScene(0);
    }
}
