using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    
    public void StartHandler()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitHandler()
    {
        Application.Quit();
    }

}
