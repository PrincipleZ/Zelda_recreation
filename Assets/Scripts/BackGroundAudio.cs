using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAudio : MonoBehaviour {

    public GameObject player;
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Player>().dead)
        {
            GetComponent<AudioSource>().enabled = false;
        }
	}
}
