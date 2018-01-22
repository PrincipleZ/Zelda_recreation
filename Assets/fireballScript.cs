using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour {

    public float moveSpeed;

    // Update is called once per frame
    void Update () {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
	}

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall" || collision.gameObject.tag == "door(wall)" || collision.gameObject.tag == "Player")
        {
            yield return new WaitForEndOfFrame();
            Destroy(this.gameObject);
        }
        else
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            yield return null;
        }
    }
}
