using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusRange : MonoBehaviour {

    public GameObject boss;
    public bool isDead = false;

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Aquamentus") == null)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            boss.GetComponent<AudioSource>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            boss.GetComponent<AudioSource>().enabled = false;
        }
    }
}
