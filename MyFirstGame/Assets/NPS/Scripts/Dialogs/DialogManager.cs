using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public GameObject dialogPanel;
    public Button[] responseButtons; //массив кнопок ответа

    private string[] currentDialog; //массив диалогов
    private int currentLine = 0;
    
    private bool isDialogActive = false;

    private void Start()
    {
        dialogPanel.SetActive(false);
        HideResponseButtons();
    }

    public void StartDialog(string[] dialog)
    {
        currentDialog = dialog;
        currentLine = 0;
        StartCoroutine(DisplayDialog());
    }

    IEnumerator DisplayDialog()
    {
        isDialogActive = true;
        dialogPanel.SetActive(true);

        foreach(char letter in currentDialog[currentLine].ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }

        yield return new WaitForSeconds(3.5f); //¬рем€ отображени€ текста
        
        //ѕереход к следующей строке или завершение диалога
        if(currentLine < currentDialog.Length - 1)
        {
            ShowResponseButtons(); //метод показа кнопок ответа
        }
        else
        {
            EndDialog();
        }


        NextLine();
    }


    public void NextLine()
    {
        currentLine++;

        if (currentLine < currentDialog.Length)
        {
            dialogText.text = "";
            StartCoroutine(DisplayDialog());
            HideResponseButtons();
        }
        else
        {
            EndDialog();
        }

    }

    public void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);
        dialogText.text = "";
    }
    private void ShowResponseButtons()
    {
        Debug.Log("Showing response buttons");
        for (int i = 0; i < responseButtons.Length; i++)
        {
            responseButtons[i].gameObject.SetActive(true);
            responseButtons[i].GetComponentInChildren<Text>().text = "Response " + (i + 1);
            int responseIndex = i;  // «ахватываем значение i дл€ передачи в делегат onClick
            responseButtons[i].onClick.RemoveAllListeners();
            responseButtons[i].onClick.AddListener(() => PlayerSelectsResponse(responseIndex));
        }
    }

    private void HideResponseButtons()
    {
        for (int i = 0; i < responseButtons.Length; i++)
        {
            //responseButtons[i].gameObject.SetActive(false);
        }
    }

    public bool IsDialogActive()
    {
        return isDialogActive;
    }

    public void PlayerSelectsResponse(int responseIndex)
    {
        Debug.Log("Player selected response: " + responseIndex);

        // Ћогика обработки выбора ответа

        // ѕереход к следующей реплике
        NextLine();
    }
}

