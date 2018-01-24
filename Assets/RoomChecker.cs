using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour {

    bool isAlive = true;
    bool checking;
    public GameObject keyPrefab;
    public AudioClip secret;
	// Update is called once per frame
	void Update () {
        if (!checking)
        {
            StartCoroutine(CheckEnemies());
        }
	}

    private void OnTriggerStay(Collider other)
    {
        isAlive = true;
    }

    IEnumerator CheckEnemies()
    {
        checking = true;
        isAlive = false;
        yield return new WaitForSeconds(.05f);
        if (!isAlive)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(secret, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        checking = false;
    }
}
