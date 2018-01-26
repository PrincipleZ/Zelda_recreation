using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MonoBehaviour
{
    public LayerMask terrain_layer;
    public float movementSpeed = 3f;
    public Vector3 path;
    public Vector3 destination;
    public float pauseProbability = .75f;
    public float pauseMin = .2f;
    public float pauseMax = 1f;

    bool doneMoving = true;
    bool validDestination = false;
    public bool timeToMove = true;

    Vector3[] candidates = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (doneMoving)
        {
            validDestination = false;
            doneMoving = false;
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
            ChooseNextDestination();
        }

        if (timeToMove)
        {
            MoveToDestination();
        }
    }

    void ChooseNextDestination()
    {
        while (!validDestination)
        {

            path = candidates[Random.Range(0, 4)];

            if (path == Vector3.right && !Physics.Raycast(transform.position, transform.right, 1f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.up && !Physics.Raycast(transform.position, transform.up, 1f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.down && !Physics.Raycast(transform.position, -transform.up, 1f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.left && !Physics.Raycast(transform.position, -transform.right, 1f, terrain_layer))
            {
                validDestination = true;
            }

        }

        destination = new Vector3(Mathf.Round(transform.position.x + path.x), Mathf.Round(transform.position.y + path.y), 0);


        if (Random.Range(0f, 1f) < pauseProbability)
        {
            timeToMove = false;
            StartCoroutine(PauseTimer(Random.Range(pauseMin, pauseMax)));
        }
    }

    void MoveToDestination()
    {
        rb.velocity = path * movementSpeed;

        if (path == Vector3.right)
        {
            if (transform.position.x >= destination.x)
            {
                transform.position = destination;
                rb.velocity = Vector3.zero;
                doneMoving = true;
            }
        }
        else if (path == Vector3.up)
        {
            if (transform.position.y >= destination.y)
            {
                transform.position = destination;
                rb.velocity = Vector3.zero;
                doneMoving = true;
            }
        }
        else if (path == Vector3.left)
        {
            if (transform.position.x <= destination.x)
            {
                transform.position = destination;
                rb.velocity = Vector3.zero;
                doneMoving = true;
            }
        }
        else
        {
            if (transform.position.y <= destination.y)
            {
                transform.position = destination;
                rb.velocity = Vector3.zero;
                doneMoving = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
//            StartCoroutine(TempIgnore(collision));
        }
    }

    IEnumerator PauseTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timeToMove = true;
    }

    IEnumerator TempIgnore(Collision collision)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        yield return new WaitForSeconds(collision.gameObject.GetComponent<Player>().flashTime);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, false);
    }
}
