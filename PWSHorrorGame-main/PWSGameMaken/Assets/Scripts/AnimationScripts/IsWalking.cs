using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsWalking : MonoBehaviour
{
    public Animator Anim;

    void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
        {
            Anim.SetBool("IsWalking", true);
        }
        else
        {
            Anim.SetBool("IsWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            Anim.SetBool("IsWalking", false);
        }
    }
}