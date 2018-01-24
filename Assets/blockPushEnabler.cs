using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockPushEnabler : MonoBehaviour {

    public bool canPush = false;

    bool checking;
    bool isAlive = true;

    void Update()
    {
        if (!checking)
        {
            StartCoroutine(CheckEnemies());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            isAlive = true;
        }
    }


    IEnumerator CheckEnemies()
    {
        checking = true;
        isAlive = false;
        yield return new WaitForSeconds(.05f);
        if (!isAlive)
        {
            canPush = true;
        }
        checking = false;
    }
}
