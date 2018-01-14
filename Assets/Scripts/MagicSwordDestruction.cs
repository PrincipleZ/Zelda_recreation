using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSwordDestruction : MonoBehaviour {

    private void Awake()
    {
        StartCoroutine(AutoKill());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall" || other.gameObject.tag == "entrance" || other.gameObject.tag == "enemy")
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
