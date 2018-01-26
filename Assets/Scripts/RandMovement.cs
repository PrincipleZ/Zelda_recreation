using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandMovement : MonoBehaviour
{
    bool doneMoving = true;
    public Vector3 path;
    public Vector3 destination;
    bool validDestination = false;

    Vector3[] candidates = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
    public LayerMask terrain_layer;

    public float movementSpeed;

    GetHurt hurtScript;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hurtScript = GetComponent<GetHurt>();
    }

    private void Update()
    {

        if (hurtScript.movement)
        {
            if (Vector3.Magnitude(new Vector3(Mathf.Abs(transform.position.x - destination.x), Mathf.Abs(transform.position.y - destination.y), 0)) > 1f)
                doneMoving = true;

            if ((path == Vector3.up && Physics.Raycast(transform.position, transform.up, 0.6f, terrain_layer)) || (path == Vector3.down && Physics.Raycast(transform.position, -transform.up, 0.6f, terrain_layer)) ||
                (path == Vector3.right && Physics.Raycast(transform.position, transform.right, 0.6f, terrain_layer)) || (path == Vector3.left && Physics.Raycast(transform.position, -transform.right, 0.6f, terrain_layer)))
            {
                doneMoving = true;
            }

            if (doneMoving)
            {
                validDestination = false;
                doneMoving = false;
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
                ChooseNextDestination();
            }

            if (rb.velocity == Vector3.zero)
            {
                doneMoving = true;
            }

            MoveToDestination();
        }
    }

    void ChooseNextDestination()
    {
        while (!validDestination)
        {

            path = candidates[Random.Range(0, 4)];

            if (path == Vector3.right && !Physics.Raycast(transform.position, transform.right, 0.6f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.up && !Physics.Raycast(transform.position, transform.up, 0.6f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.down && !Physics.Raycast(transform.position, -transform.up, 0.6f, terrain_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.left && !Physics.Raycast(transform.position, -transform.right, 0.6f, terrain_layer))
            {
                validDestination = true;
            }

        }

        destination = new Vector3(Mathf.Round(transform.position.x + path.x), Mathf.Round(transform.position.y + path.y), 0);

        rb.velocity = path * movementSpeed;
    }

    void MoveToDestination()
    {
        rb.velocity = path * movementSpeed;

        if (path == Vector3.right)
        {
            if (transform.position.x + (rb.velocity.x * Time.deltaTime) >= destination.x)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else if (path == Vector3.up)
        {
            if (transform.position.y + (rb.velocity.y * Time.deltaTime) >= destination.y)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else if (path == Vector3.left)
        {
            if (transform.position.x + (rb.velocity.x * Time.deltaTime) <= destination.x)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else
        {
            if (transform.position.y + (rb.velocity.y * Time.deltaTime) <= destination.y)
            {
                transform.position = destination;
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

    IEnumerator TempIgnore(Collision collision)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        yield return new WaitForSeconds(collision.gameObject.GetComponent<Player>().flashTime);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, false);
    }
}
