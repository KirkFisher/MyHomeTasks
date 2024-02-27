using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    [SerializeField] private GameObject wood;
    private Finish finish;
    private Animator animator;
    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        animator = GetComponent<Animator>();
    }
    public void ActivateLeverArm()
    {
        finish.Activate();
        animator.SetBool("isActivate", true);
        wood.SetActive(false);

        ActivatePlatform platform = FindObjectOfType<ActivatePlatform>();
        platform.ActivatedPlatform();
    }
}
