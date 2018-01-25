using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSwordDestruction : MonoBehaviour {

//    public AudioClip magicSwordSound;

    private void Awake()
    {
        StartCoroutine(AutoKill());
//        AudioSource.PlayClipAtPoint(magicSwordSound, Camera.main.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "AquamentusBody" && this.tag == "arrow")
        {

        }
        else if (other.gameObject.tag == "wall" || other.gameObject.tag == "door(wall)" || other.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator AutoKill()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
