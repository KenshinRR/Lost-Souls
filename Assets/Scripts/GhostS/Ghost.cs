using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.AddComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
