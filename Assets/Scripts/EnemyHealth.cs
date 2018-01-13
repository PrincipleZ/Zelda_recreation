using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;
    bool damageTaken;
    bool invincible = false;
    int damageAmount;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update () {
        if (damageTaken && !invincible)
        {
            currentHealth -= damageAmount;
            StartCoroutine(DamageColor());

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
        }else if(other_go.tag == "arrow")
        {
            damageTaken = true;
            damageAmount = 2;
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
}
