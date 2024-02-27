using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ActivatedPlatform()
    {
        LeverArm leverArm = FindObjectOfType<LeverArm>();
        if (leverArm != null)
        {
            animator.SetBool("isActivated", true);
        } 
    }
}
