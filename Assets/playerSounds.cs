using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour {
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip lowHealth;
    public AudioClip counting;
    public AudioClip shootingArrow;
    public AudioClip placeBomb;

    bool nextBeep = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Player>().currentHealth <= 2 && !GetComponent<Player>().dead)
        {
            if(nextBeep)
                StartCoroutine(LowHealthBeep());
        }
	}

    IEnumerator LowHealthBeep()
    {
        nextBeep = false;
        AudioSource.PlayClipAtPoint(lowHealth, Camera.main.transform.position);
        yield return new WaitForSeconds(.3f);
        nextBeep = true;
    }

    public void GotHit()
    {
        AudioSource.PlayClipAtPoint(hurt, Camera.main.transform.position);
    }

    public void ShotArrow()
    {
        AudioSource.PlayClipAtPoint(shootingArrow, Camera.main.transform.position);
    }

    public void DeathAudio()
    {
        AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
    }

    public void DropBomb()
    {
        AudioSource.PlayClipAtPoint(placeBomb, Camera.main.transform.position);
    }

    public IEnumerator MultipleCollected(int amount)
    {
        while(amount > 0)
        {
            AudioSource.PlayClipAtPoint(counting, Camera.main.transform.position);
            yield return new WaitForSeconds(.05f);
            amount--;
        }

    }
}
