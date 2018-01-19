using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapMovement : MonoBehaviour
{

    public float attackSpeed;
    public float returnSpeed;
    public LayerMask playerLayer;
    public bool up;
    public bool down;
    public bool right;
    public bool left;
    public float vertDistance = 4.75f;
    public float horizDistance = 2.625f;

    Vector3 direction;
    Vector3 destination;
    Vector3 startingPos;

    bool attacking = false;
    bool returning = false;

    // Use this for initialization
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!attacking && !returning)
        {
            if (up && Physics.SphereCast(new Ray(transform.position, transform.up), .5f, 6f, playerLayer))
            {
                destination = startingPos + new Vector3(0, vertDistance, 0);
                direction = Vector3.up * attackSpeed;
                attacking = true;
            }
            else if (right && Physics.SphereCast(new Ray(transform.position, transform.right), .5f, 11f, playerLayer))
            {
                destination = startingPos + new Vector3(horizDistance, 0, 0);
                direction = Vector3.right * attackSpeed;
                attacking = true;
            }
            else if (down && Physics.SphereCast(new Ray(transform.position, -transform.up), .5f, 6f, playerLayer))
            {
                destination = startingPos - new Vector3(0, vertDistance, 0);
                direction = Vector3.down * attackSpeed;
                attacking = true;
            }
            else if (left && Physics.SphereCast(new Ray(transform.position, -transform.right), .5f, 11f, playerLayer))
            {
                destination = startingPos - new Vector3(horizDistance, 0, 0);
                direction = Vector3.left * attackSpeed;
                attacking = true;
            }
        }

        if (attacking)
        {
            MoveToDestination();
        }
        if (returning)
        {
            MoveToOrigin();
        }
    }

    void MoveToDestination()
    {
        if(direction.x > 0)
        {
            if(transform.position.x > destination.x)
            {
                transform.position = destination;
                returning = true;
                attacking = false;
                direction = Vector3.left * returnSpeed;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else if(direction.x < 0)
        {
            if(transform.position.x < destination.x)
            {
                transform.position = destination;
                returning = true;
                attacking = false;
                direction = Vector3.right * returnSpeed;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else if(direction.y > 0)
        {
            if(transform.position.y > destination.y)
            {
                transform.position = destination;
                returning = true;
                attacking = false;
                direction = Vector3.down * returnSpeed;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else
        {
            if(transform.position.y < destination.y)
            {
                transform.position = destination;
                returning = true;
                attacking = false;
                direction = Vector3.up * returnSpeed;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
    }

    void MoveToOrigin()
    {
        if (direction.x > 0)
        {
            if (transform.position.x > startingPos.x)
            {
                transform.position = startingPos;
                returning = false;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else if (direction.x < 0)
        {
            if (transform.position.x < startingPos.x)
            {
                transform.position = startingPos;
                returning = false;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else if (direction.y > 0)
        {
            if (transform.position.y > startingPos.y)
            {
                transform.position = startingPos;
                returning = false;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y < startingPos.y)
            {
                transform.position = startingPos;
                returning = false;
            }
            else
            {
                transform.Translate(direction * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BladeTrapMovement>())
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
        }
        else if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(TempIgnore(collision.gameObject));
        }
    }

    IEnumerator TempIgnore(GameObject player)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        yield return new WaitForSeconds(player.GetComponent<Player>().flashTime);
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
    }
}
