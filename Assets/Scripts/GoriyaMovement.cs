using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaMovement : MonoBehaviour
{

    public LayerMask terrain_layer;
    public LayerMask obst_layer;
    public float movementSpeed;
    public Vector3 path;
    public Vector3 destination;
    public float boomerangFrequency = .005f;
	public GameObject boomerPrefab;
    public bool boomCanary = false;
    public bool hasDied = false;

    bool validDestination = false;
    bool throwing = false;
    bool doneMoving = true;
    bool boomerSpawned;

    Vector3[] candidates = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    GameObject boomer;
    GetHurt hurtScript;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hurtScript = GetComponent<GetHurt>();
    }

    private void Update()
    {
        if(!throwing && hasDied)
        {
            Destroy(boomer);
            Destroy(this.gameObject);
            Debug.Log("happenedhere");
        }

        if (hurtScript.movement && !throwing && rb.isKinematic == false)
        {
            if (Vector3.Magnitude(new Vector3(Mathf.Abs(transform.position.x - destination.x), Mathf.Abs(transform.position.y - destination.y), 0)) > 1f)
                doneMoving = true;

            if ((path == Vector3.up && (Physics.Raycast(transform.position, transform.up, 0.6f, terrain_layer) || Physics.Raycast(transform.position, transform.up, 0.6f, obst_layer))) || 
                    (path == Vector3.down && (Physics.Raycast(transform.position, -transform.up, 0.6f, terrain_layer) || Physics.Raycast(transform.position, -transform.up, 0.6f, obst_layer))) ||
                (path == Vector3.right && (Physics.Raycast(transform.position, transform.right, 0.6f, terrain_layer) || Physics.Raycast(transform.position, transform.right, 0.6f, obst_layer))) || 
                (path == Vector3.left && (Physics.Raycast(transform.position, -transform.right, 0.6f, terrain_layer) || Physics.Raycast(transform.position, -transform.right, 0.6f, obst_layer))))
            {
                doneMoving = true;
            }

            if (doneMoving)
            {
                validDestination = false;
                doneMoving = false;
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y) + .1f, 0);
                ChooseNextDestination();
            }

            if (rb.velocity == Vector3.zero)
            {
                doneMoving = true;
            }

            MoveToDestination();

            if (Random.Range(0f, 1f) < boomerangFrequency)
            {
                throwing = true;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (!boomerSpawned)
            {
                StartCoroutine(ThrowBoomerang());
                boomerSpawned = true;
            }
        }
    }

    void ChooseNextDestination()
    {
        while (!validDestination)
        {

            path = candidates[Random.Range(0, 4)];

            if (path == Vector3.right && !Physics.Raycast(transform.position, transform.right, 0.6f, terrain_layer) && !Physics.Raycast(transform.position, transform.right, 0.6f, obst_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.up && !Physics.Raycast(transform.position, transform.up, 0.6f, terrain_layer) && !Physics.Raycast(transform.position, transform.up, 0.6f, obst_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.down && !Physics.Raycast(transform.position, -transform.up, 0.6f, terrain_layer) && !Physics.Raycast(transform.position, -transform.up, 0.6f, obst_layer))
            {
                validDestination = true;
            }

            else if (path == Vector3.left && !Physics.Raycast(transform.position, -transform.right, 0.6f, terrain_layer) && !Physics.Raycast(transform.position, -transform.right, 0.6f, obst_layer))
            {
                validDestination = true;
            }

        }

        destination = new Vector3(Mathf.Round(transform.position.x + path.x), Mathf.Round(transform.position.y + path.y) + .1f, 0);

        rb.velocity = path * movementSpeed;
    }

    void MoveToDestination()
    {
        rb.velocity = path * movementSpeed;

        if (path == Vector3.right)
        {
            if (transform.position.x >= destination.x)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else if (path == Vector3.up)
        {
            if (transform.position.y >= destination.y)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else if (path == Vector3.left)
        {
            if (transform.position.x <= destination.x)
            {
                transform.position = destination;
                doneMoving = true;
            }
        }
        else
        {
            if (transform.position.y <= destination.y)
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
            StartCoroutine(TempIgnore(collision));
        }
    }

    IEnumerator ThrowBoomerang()
    {
        rb.isKinematic = true;
		boomer = (GameObject)Instantiate (boomerPrefab, transform.position + path, Quaternion.identity);
		boomer.GetComponent<BoomerEnemy> ().shoot (path, transform);
        while (!boomer.GetComponent<BoomerEnemy>().boomerCanary)
        {
            if (hasDied)
            {
                Destroy(boomer);
                Destroy(this.gameObject);
                Debug.Log("happenedhere2");
            }

            yield return null;
            if (boomer.GetComponent<BoomerEnemy>().back)
            {
                if(Vector3.Distance(transform.position, boomer.transform.position) < .5f)
                {
                    boomer.GetComponent<BoomerEnemy>().boomerCanary = true;
                    if (hasDied)
                    {
                        Destroy(boomer);
                        Destroy(this.gameObject);
                        Debug.Log("happenedhere3");
                    }
                }
            }
        }
        Destroy(boomer);
        throwing = false;
        boomerSpawned = false;
        rb.isKinematic = false;
    }

    IEnumerator TempIgnore(Collision collision)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, true);
        yield return new WaitForSeconds(collision.gameObject.GetComponent<Player>().flashTime);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), collision.collider, false);
    }
}
