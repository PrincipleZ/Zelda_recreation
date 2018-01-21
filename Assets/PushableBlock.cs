using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour {

    public bool beenPushed = false;
    public float totalTimeToPush;

    bool inContact = false;
    public float timePushing;
    int pushingNESW;
    int finalNESW;
    float timeInMove = 0;
	
	// Update is called once per frame
	void Update () {
        if (!beenPushed)
        {
            if (!inContact)
                timePushing = 0;
            else
                timePushing += Time.deltaTime;

            if (timePushing >= totalTimeToPush)
            {
                beenPushed = true;
                finalNESW = pushingNESW;
            }
               
        }
        else
        {
            if (timeInMove <= 1f)
            {
                MoveBlock();
                timeInMove += Time.deltaTime;
            }
        }
	}


    void MoveBlock()
    {
        if (finalNESW == 0)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if (finalNESW == 1)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (finalNESW == 2)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        else if (finalNESW == 3)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else
        {
            Debug.Log("no direction chosen");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            inContact = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {

                Debug.Log("outer");
                if (pushingNESW != 0)
                {
                    timePushing = 0;
                }
                pushingNESW = 0;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                if (pushingNESW != 1)
                {
                    timePushing = 0;
                }
                pushingNESW = 1;
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                if (pushingNESW != 2)
                {
                    timePushing = 0;
                }
                pushingNESW = 2;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                if (pushingNESW != 3)
                {
                    timePushing = 0;
                }
                pushingNESW = 3;
            }
            else
            {
                pushingNESW = 5;
                timePushing = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            inContact = false;
        }
    }

}
