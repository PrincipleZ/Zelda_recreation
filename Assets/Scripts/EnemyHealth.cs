using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
	GetHurt hurtScript;

    public AudioClip deathSoundClip;
    public AudioClip hurtSound;

    bool invincible = false;
    bool isHoriz;


    private void Start()
    {
        currentHealth = maxHealth;
		hurtScript = GetComponent<GetHurt> ();
    }

    void Update () {
        if (hurtScript.damageTaken && !invincible)
        {
			currentHealth -= hurtScript.damageAmount;
			StartCoroutine (DamageColor ());
			hurtScript.damageTaken = false;

            if(currentHealth <= 0)
            {
                AudioSource.PlayClipAtPoint(deathSoundClip, Camera.main.transform.position);
                if (GetComponent<DropItems>() != null)
                    GetComponent<DropItems>().DropChance();
                if (transform.childCount > 0)
                    transform.DetachChildren();
                if(GetComponent<GoriyaMovement>() != null)
                {
                    GetComponent<GoriyaMovement>().hasDied = true;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);
            }
        }

	}
    //IMPLEMENT WHEN WE HAVE WEAPONS
    

    IEnumerator DamageColor()
    {
        invincible = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        invincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
