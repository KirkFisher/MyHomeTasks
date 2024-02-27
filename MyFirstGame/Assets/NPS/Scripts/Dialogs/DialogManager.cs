using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Animator animator;
    public GameObject tradeButton; // ������ �� ������ ��� ��������
    public GameObject shopCanvas; // ������ �� ��������� ��������
    public AttackController attackController;

    private bool isDialogActive = false;
    private bool dialogCooldown = false; // ����, ����������� �� ��, ��� ������ ��������� � ������ ��������
    private float dialogCooldownTime = 0.5f; // ����� �������� � ��������
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        ShowTradeButton(false);
        ShowShopCanvas(false);
    }

    public void StartDialog(Dialog dialog)
    {
        if (isDialogActive || dialogCooldown) return;
        isDialogActive = true;
        animator.SetBool("isOne", true);
        nameText.text = dialog.name;
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        // ������������ ����������� ���������
        if (attackController != null)
        {
            attackController.enabled = false;
        }
    }
    public void StartDialogWithMerchant(Dialog dialog)
    {
        StartDialog(dialog);
        ShowTradeButton(true);
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentance)
    {
        dialogText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    public void EndDialog()
    {
        animator.SetBool("isOne", false);
        isDialogActive = false;
        ShowTradeButton(false);
        // ��������� ��� ������
        StartCoroutine(DialogCooldown());
        // ���������� ����������� ���������
        if (attackController != null)
        {
            attackController.enabled = true;
        }
    }
    IEnumerator DialogCooldown()
    {
        dialogCooldown = true;
        yield return new WaitForSeconds(dialogCooldownTime);
        dialogCooldown = false;
    }

    public void EndDialogExternally()
    {
        EndDialog();
    }

    public void StartTrading()
    {
        // ����� ��������� ������ ��� ��������, ��������� �������
        ShowShopCanvas(true);
        EndDialog();
    }

    private void ShowTradeButton(bool show)
    {
        if (tradeButton != null)
        {
            tradeButton.SetActive(show);
        }
    }

    private void ShowShopCanvas(bool show)
    {
        
       shopCanvas.SetActive(true);
        
    }
}
