using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KesseMovement : MonoBehaviour {

    public float maxPathTime = 8f;
    public float moveSpeed = 3f;
    public float maxFlightTime = 20f;
    public float minFlightTime = 1f;
    public float timeToAccelerate = 2f;
    public float restTime = 1f;

    Rigidbody rb;
    Vector3 direction;
    float distance;
    float distanceTraveled;
    bool hasDirection = false;
    bool inCollision;
    float flightTime;
    float timeInCurrentFlight = 0f;
    float acceleration = 0;
    bool isDecelerating = false;
    bool inFlight = false;
    bool waiting = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!inFlight)
        {
            flightTime = Random.Range(minFlightTime, maxFlightTime);
            acceleration = 0f;
            timeInCurrentFlight = 0f;
            inFlight = true;
        }

        if(timeInCurrentFlight < timeToAccelerate)
        {
            acceleration = (float)(timeInCurrentFlight / timeToAccelerate);
        }
        
        if(acceleration > 1f && !isDecelerating)
        {
            acceleration = 1f;
        }

        if(flightTime - timeInCurrentFlight < timeToAccelerate)
        {
            isDecelerating = true;
            acceleration = (flightTime - timeInCurrentFlight)/timeToAccelerate;

        }

        if(acceleration < 0f)
        {
            acceleration = 0f;
            if (!waiting)
            {
                waiting = true;
                StartCoroutine(AtRest());
            }
            
        }
    }

    private void FixedUpdate()
    {

        timeInCurrentFlight += Time.deltaTime;

        if (!hasDirection)
        {
            ChooseDirection();
            distanceTraveled = 0f;
        }

        rb.velocity = direction * moveSpeed * acceleration;
        distanceTraveled += Mathf.Abs(Vector3.Magnitude(rb.velocity)) * Time.deltaTime;

        if (distanceTraveled >= distance)
        {
            hasDirection = false;
        }
    }

    void ChooseDirection()
    {
        int randInt = Random.Range(0, 4);
        if (randInt == 0)
        {
            direction = Vector3.left;
        }
        else if (randInt == 1)
        {
            direction = new Vector3(-1f, 1f, 0);
        }
        else if (randInt == 2)
        {
            direction = Vector3.up;
        }
        else
        {
            direction = new Vector3(1f, 1f, 0);
        }
        randInt = Random.Range(0, 2);
        if (randInt == 0)
        {
            direction *= -1;
        }

        distance = Random.Range(1f, maxPathTime);
        hasDirection = true;
        rb.velocity = direction * moveSpeed;
    }

    IEnumerator AtRest()
    {
        yield return new WaitForSeconds(restTime);
        inFlight = false;
        waiting = false;
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
        else if(collision.gameObject.tag == "Player")
        {
            //knockback and damage
        }
        else
        {
            if (!inCollision)
            {
                inCollision = true;
                distanceTraveled -= .1f;
                direction *= -1;
                yield return new WaitForFixedUpdate();
                yield return new WaitForFixedUpdate();
                inCollision = false;
            }
        }
    }
}
