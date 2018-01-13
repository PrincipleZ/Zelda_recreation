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
        if (other.GetComponent<EnemyHealth>())
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
