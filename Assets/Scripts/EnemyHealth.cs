using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
	public float force = 300f;
	public bool movement = true;
	public float knockBackTime = 0.3f;
    bool damageTaken;
    bool invincible = false;
    int damageAmount;
	Rigidbody rb;
    bool isHoriz;


    private void Start()
    {
        currentHealth = maxHealth;
		rb = GetComponent<Rigidbody> ();
    }

    void Update () {
        if (damageTaken && !invincible)
        {
            currentHealth -= damageAmount;
			StartCoroutine (DamageColor ());
            damageTaken = false;

            if(currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }

	}
    //IMPLEMENT WHEN WE HAVE WEAPONS
    private void OnTriggerEnter(Collider other)
    {
        GameObject other_go = other.gameObject;
        if(other_go.tag == "sword")
        {
            damageTaken = true;
            damageAmount = 1;
			StartCoroutine(KnockBack(other));
        }else if(other_go.tag == "arrow")
        {
            damageTaken = true;
            damageAmount = 2;
			StartCoroutine(KnockBack(other));

        }
        else if (other_go.tag == "magicSword")
        {
            damageTaken = true;
            damageAmount = 1;
			StartCoroutine(KnockBack(other));
        }
    }

    IEnumerator DamageColor()
    {
        invincible = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.5f);
        invincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

	IEnumerator KnockBack(Collider collider){
		movement = false;
		Vector3 knockBackDir = (transform.position - collider.ClosestPointOnBounds(transform.position)).normalized;
		//Normalize to only grid knockback
		Debug.Log(knockBackDir);
        if(Mathf.Abs(knockBackDir.x) > Mathf.Abs(knockBackDir.y))
        {
            isHoriz = true;
        }
        else
        {
            isHoriz = false;
        }

        if (GetComponent<RandMovement>().path.x != 0)
        {
            if (isHoriz)
            {
                knockBackDir.y = 0;
                if (knockBackDir.x < 0)
                {
                    knockBackDir.x = -1;
                }
                else
                {
                    knockBackDir.x = 1;
                }
            }
            else
            {
                knockBackDir = Vector2.zero;
            }

        }
        else
        {
            if (!isHoriz)
            {
                knockBackDir.x = 0;
                if (knockBackDir.y < 0)
                {
                    knockBackDir.y = -1;
                }
                else
                {
                    knockBackDir.y = 1;
                }
            }
            else
            {
                knockBackDir = Vector2.zero;
            }
        }
        if(knockBackDir != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(force * knockBackDir);

            yield return new WaitForSeconds(knockBackTime);
        }

		movement = true;
	}
}
