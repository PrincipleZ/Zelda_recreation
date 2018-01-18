using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour {

    Animator animator;
	Player playerScript;
    SwordDirection swordScript;
    Bow bowScript;
	Animation animation;
	AnimatorClipInfo[] clips;
    ArrowKeyMovement movementScript;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		playerScript = GetComponent<Player> ();
        swordScript = GetComponent<SwordDirection>();
        bowScript = GetComponent<Bow>();
        movementScript = GetComponent<ArrowKeyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
			
		if(!playerScript.dead){
			animator.SetFloat ("horizontal_input", Input.GetAxisRaw ("Horizontal"));
			animator.SetFloat ("vertical_input", Input.GetAxisRaw ("Vertical"));
            animator.SetInteger("direction_NESW", swordScript.directionFacingNESW);
            animator.SetBool("is_stabbing", swordScript.isSwingingSword);
			animator.SetBool ("invincible", playerScript.invincible);
            animator.SetBool("is_shooting", bowScript.isShooting);
            animator.SetBool("is_doing_action", movementScript.isDoingAction);
			clips = animator.GetCurrentAnimatorClipInfo (0);
			if (Input.GetAxisRaw ("Horizontal") == 0 && Input.GetAxisRaw ("Vertical") == 0){
				animator.SetBool ("is_moving", false);
			}else{
				animator.SetBool ("is_moving", true);
			}
			if (playerScript.invincible){
				animator.SetLayerWeight (1, 1f);
			}else{
				animator.SetLayerWeight (1, 0);
			}
			if (!animator.GetBool("is_moving") && !playerScript.invincible && clips.Length == 1 && clips[0].clip.name.StartsWith("run") && !swordScript.isSwingingSword) {
				Debug.Log (playerScript.invincible);
				animator.speed = 0.0f;
			} else {
				animator.speed = 1.0f;
			}
		}
}
}


