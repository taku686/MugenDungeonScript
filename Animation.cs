using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//komennto
public class Animation : MonoBehaviour
{
    private Animator animator;
  
    private void Start()
    {
        animator = GetComponent<Animator>();
      
    }

    public void AttackAnim()
    {
        animator.SetTrigger("attack");
    }
}
