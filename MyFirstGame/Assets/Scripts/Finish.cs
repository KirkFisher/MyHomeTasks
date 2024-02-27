using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject messageUI;
    public bool isActivated = false;
    public void Activate()
    {
        isActivated = true;
        messageUI.SetActive(true);
    }
    public void FinishLevel()
    {
        if (isActivated)
        {
            gameObject.SetActive(false);
            levelCompleteCanvas.SetActive(true);
        }
        else
        {
            messageUI.SetActive(false);
        }
    }
}
