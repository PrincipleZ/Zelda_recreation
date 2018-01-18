using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandMovement : MonoBehaviour
{
    bool isHorizontal;
    float direction;
    bool doneMoving = true;
    public Vector3 path;
    public Vector3 destination;
    bool validDestination = false;

    public GameObject north;
    public GameObject south;
    public GameObject east;
    public GameObject west;
    public float movementSpeed;

	GetHurt hurtScript;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
		hurtScript = GetComponent<GetHurt> ();
    }

    private void Update()
    {
		if (hurtScript.movement)
        {
            if (Vector3.Magnitude(new Vector3(Mathf.Abs(transform.position.x - destination.x), Mathf.Abs(transform.position.y - destination.y), 0)) > 1f)
                doneMoving = true;

            if((path == Vector3.up && !north.GetComponent<AvailableSquare>().isAvailable) || (path == Vector3.down && !south.GetComponent<AvailableSquare>().isAvailable) || 
                (path == Vector3.right && !east.GetComponent<AvailableSquare>().isAvailable) || (path == Vector3.left && !west.GetComponent<AvailableSquare>().isAvailable))
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

            if(rb.velocity == Vector3.zero)
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
            if (Random.value < .5f)
                isHorizontal = true;
            else
                isHorizontal = false;

            if (Random.value < .5f)
                direction = 1f;
            else
                direction = -1f;

            if (isHorizontal && direction == 1f && east.GetComponent<AvailableSquare>().isAvailable)
            {
                validDestination = true;
            }
                
            else if (isHorizontal && direction == -1f && west.GetComponent<AvailableSquare>().isAvailable)
            {
                validDestination = true;
            }
                
            else if (!isHorizontal && direction == 1f && north.GetComponent<AvailableSquare>().isAvailable)
            {
                validDestination = true;
            }
                
            else if (!isHorizontal && direction == -1f && south.GetComponent<AvailableSquare>().isAvailable)
            {
                validDestination = true;
            }
                    
        }
        

        if (isHorizontal)
            path = new Vector3(1f, 0, 0);
        else
            path = new Vector3(0, 1f, 0);

        path *= direction;

        destination = new Vector3(Mathf.Round(transform.position.x + path.x), Mathf.Round(transform.position.y + path.y), 0);

        rb.velocity = path * movementSpeed;
    }

    void MoveToDestination()
    {
        rb.velocity = path * movementSpeed;
        if (direction == 1)
        {
            if (isHorizontal)
            {
                if(transform.position.x + (rb.velocity.x * Time.deltaTime) >= destination.x)
                {
                    transform.position = destination;
                    doneMoving = true;
                }
            }
            else
            {
                if (transform.position.y + (rb.velocity.y * Time.deltaTime) >= destination.y)
                {
                    transform.position = destination;
                    doneMoving = true;
                }
            }
        }
        else
        {
            if (isHorizontal)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider);
        }
        else if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(TempIgnore(collision));
        }
    }

    IEnumerator TempIgnore(Collision collision)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        yield return new WaitForSeconds(1f);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, false);
    }
}
