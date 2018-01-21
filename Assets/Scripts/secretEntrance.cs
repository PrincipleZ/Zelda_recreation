using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretEntrance : MonoBehaviour {

    public GameObject block;
    public GameObject openDoor;
    public Sprite s;
    

    // Update is called once per frame
    void Update () {
        if (block.GetComponent<PushableBlock>().doneMoving)
        {
            GameObject temp = Instantiate(openDoor, transform.position, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sprite = s;
            gameObject.SetActive(false);
        }
	}
}
