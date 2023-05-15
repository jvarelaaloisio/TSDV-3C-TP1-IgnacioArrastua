using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField]private Animator animator;
    public bool isActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActiveBox()
    {
        if (isActive) return;
            isActive = true;
        animator.SetTrigger("Pop");
    } 
    public void DeactivateBox()
    {
        isActive = false;
        animator.SetTrigger("Close");
    }
  
    
}
