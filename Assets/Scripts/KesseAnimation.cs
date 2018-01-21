using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KesseAnimation : MonoBehaviour
{

    Animator animator;
    KesseMovement movementScript;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<KesseMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = movementScript.acceleration;

    }
}


