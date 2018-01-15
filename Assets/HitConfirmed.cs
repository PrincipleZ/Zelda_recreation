using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitConfirmed : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            player.GetComponent<SwordDirection>().swordHitEnemy = true;
        }
    }
}
