using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPSManager : MonoBehaviour
{
    public DialogManager dialogManager;
    public string[] dialogLines;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !dialogManager.IsDialogActive())
        {
            //dialogManager.StartDialog(dialogLines);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    private void Update()
    {
        if (dialogManager.IsDialogActive() && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.NextLine();
        }

        if (!dialogManager.IsDialogActive() && Input.GetKeyDown(KeyCode.E))
        {
            // �������� �� ���������� ������� � ������� ������� "E"
            // ��� ����� ���� �������� ������ ��� ������ �������
            dialogManager.StartDialog(dialogLines);
        }
    }
}
