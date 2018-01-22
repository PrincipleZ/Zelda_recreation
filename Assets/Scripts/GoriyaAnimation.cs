using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaAnimation : MonoBehaviour
{

    Animator animator;
    GoriyaMovement movementScript;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<GoriyaMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movementScript.path.y == 1)
        {
            animator.SetInteger("direction_NESW", 0);
        }
        else if (movementScript.path.x == 1)
        {
            animator.SetInteger("direction_NESW", 1);
        }
        else if (movementScript.path.y == -1)
        {
            animator.SetInteger("direction_NESW", 2);
        }
        else
        {
            animator.SetInteger("direction_NESW", 3);
        }

    }
}


