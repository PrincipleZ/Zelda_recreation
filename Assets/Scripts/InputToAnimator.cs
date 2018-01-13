﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour {

    Animator animator;
	Player playerScript;
    SwordDirection swordScript;
	Animation animation;
	AnimatorClipInfo[] clips;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		playerScript = GetComponent<Player> ();
        swordScript = GetComponent<SwordDirection>();
	}
	
	// Update is called once per frame
	void Update () {
			
		if(!playerScript.dead){
			animator.SetFloat ("horizontal_input", Input.GetAxisRaw ("Horizontal"));
			animator.SetFloat ("vertical_input", Input.GetAxisRaw ("Vertical"));
            animator.SetInteger("direction_NESW", swordScript.directionFacingNESW);
            animator.SetBool("is_stabbing", swordScript.isSwingingSword);
			animator.SetBool ("invincible", playerScript.invincible);
			clips = animator.GetCurrentAnimatorClipInfo (0);
			if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0 && !playerScript.invincible && clips.Length == 1 && clips[0].clip.name.StartsWith("run") ) {
				animator.speed = 0.0f;
			} else {
				animator.speed = 1.0f;
			}
		}
}
}


