using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretEntrance : MonoBehaviour {

    public GameObject block;
    public GameObject openDoor;
    public Sprite s;
    public AudioClip secretSoundClip;

    // Update is called once per frame
    void Update () {
        if (block.GetComponent<PushableBlock>().doneMoving)
        {
            GameObject temp = Instantiate(openDoor, transform.position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sprite = s;
            AudioSource.PlayClipAtPoint(secretSoundClip, Camera.main.transform.position);
            gameObject.SetActive(false);
        }
	}
}
