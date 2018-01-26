using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour {
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip counting;
    public AudioClip shootingArrow;
    public AudioClip placeBomb;

    public AudioSource beamSource;
    public AudioSource InvItem;
    public AudioSource fanfare;
    public AudioSource healthIsLow;

    bool nextBeep = true;
	
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
        healthIsLow.Play();
        yield return new WaitForSeconds(.3f);
        nextBeep = true;
    }

    public void GotNewWeapon()
    {
        InvItem.Play();
        fanfare.Play();
    }

    public void GotNewWeapon2()
    {
        InvItem.Play();
    }

    public void SwordBeam()
    {
        beamSource.Play();
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
