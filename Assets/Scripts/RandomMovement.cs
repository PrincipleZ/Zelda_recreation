using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {


    public float movement_speed = 4;
    public float movement_bias = .1f;

    int NESW;
    Vector3 endPos;
    Vector3 walkDirection;
    bool doneMoving = true;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NESW = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if(doneMoving)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

            if (Random.Range(0f, 1f) > movement_bias)
            {
                NESW = Random.Range(0, 4);
            }
            
            if(NESW == 0)
            {
                walkDirection = new Vector3(0, 1f, 0);
            }
            else if(NESW == 1)
            {
                walkDirection = new Vector3(1f, 0, 0);
            }
            else if (NESW == 2)
            {
                walkDirection = new Vector3(0, -1f, 0);
            }
            else
            {
                walkDirection = new Vector3(-1f, 0, 0);
            }
            endPos = transform.position + walkDirection;
            
            doneMoving = false;
        }

        if (NESW == 0 || NESW == 1)
        {
            if (Mathf.Floor(transform.position.x) == Mathf.Floor(endPos.x) && Mathf.Floor(transform.position.y) == Mathf.Floor(endPos.y))
            {
                doneMoving = true;
                transform.position = new Vector3(Mathf.Floor(endPos.x), Mathf.Floor(endPos.y), Mathf.Floor(endPos.z));
            }
            else
            {
                rb.velocity = movement_speed * walkDirection;
            }
        }
        else
        {
            if (Mathf.Ceil(transform.position.x) == endPos.x && Mathf.Ceil(transform.position.y) == endPos.y)
            {
                doneMoving = true;
                transform.position = new Vector3(Mathf.Floor(endPos.x), Mathf.Floor(endPos.y), Mathf.Floor(endPos.z));
            }
            else
            {
                rb.velocity = movement_speed * walkDirection;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collector>() == null)
        {
            doneMoving = true;
            Debug.Log("here");
        }
    }

}
