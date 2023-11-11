using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRunning : MonoBehaviour
{
    [SerializeField] private Animator Anim;

    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }
    }
}
