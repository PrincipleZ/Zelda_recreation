using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapdoor : MonoBehaviour {

    public GameObject lockedDoor;
    public Sprite s;
    public AudioClip doorSound;

    GameObject temp;
    bool isAlive = true;
    bool hasEntered = false;
	bool checking = false;
	
	// Update is called once per frame
	void Update () {
        if (!checking)
        {
            StartCoroutine(CheckEnemies());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "enemy")
        {
            isAlive = true;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            AudioSource.PlayClipAtPoint(doorSound, Camera.main.transform.position);
            temp = Instantiate(lockedDoor, transform.position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sprite = s;
        }
    }

    IEnumerator CheckEnemies()
    {
        checking = true;
        isAlive = false;
        yield return new WaitForSeconds(.05f);
        if (!isAlive)
        {
            AudioSource.PlayClipAtPoint(doorSound, Camera.main.transform.position);
            Destroy(temp);
            Destroy(this.gameObject);
        }
        checking = false;
    }
}
