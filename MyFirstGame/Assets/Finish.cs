using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private bool isActivated = false;
    public void FinishLevel() 
    {
        if (isActivated)
        {
         gameObject.SetActive(false);

        }
    }
}
