using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyctheTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("isReaping", true);
    }
}
