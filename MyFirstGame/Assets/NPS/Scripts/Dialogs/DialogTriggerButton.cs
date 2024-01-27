using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTriggerButton : MonoBehaviour
{
    public Dialog dialog;
    public Button startDialogButton;
    public Animator animator;

    private bool isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger Enter: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            if (!isPlayerInside)
            {
                isPlayerInside = true;

                if (animator != null)
                {
                    animator.SetBool("withNPS", true);
                }

                if (startDialogButton != null)
                {
                    startDialogButton.gameObject.SetActive(true);
                }/*
                else
                {
                    Debug.LogError("StartDialogButton is not assigned!");
                }
                */
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Trigger Exit: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            if (isPlayerInside)
            {
                isPlayerInside = false;

                if (animator != null)
                {
                    animator.SetBool("withNPS", false);
                }

                if (startDialogButton = null)
                {
                    startDialogButton.gameObject.SetActive(false);
                }/*
                else
                {
                    Debug.LogError("StartDialogButton is not assigned!");
                }*/
            }
        }
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}

