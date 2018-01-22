using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusScript : MonoBehaviour {

    public GameObject player;

    public float moveSpeed = 1f;
    public GameObject fireball;
    public float attackTimeMin = 1.5f;
    public float attackTimeMax = 5f;

    float rangeMin;
    float rangeMax;

    Vector3 playerPosition;
    public Vector3 angle;
    public float multiplyer;
    public float yValue;
    float timeUntilAttack;
    bool startAttacking = true;
    bool isAttacking = false;
    float distance;
    float destination;
    bool destinationChosen = false;
    bool larger;
	// Use this for initialization
	void Start () {
        rangeMin = transform.position.x - 2.25f;
        rangeMax = transform.position.x + 2.25f;
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = player.transform.position;
        angle = Vector3.Normalize(transform.position - playerPosition);
        multiplyer = Mathf.Round(angle.y * 4);
        if(angle.x < 0)
        {
            yValue = 1;
        }
        else
        {
            yValue = -1;
        }
        if (!destinationChosen)
        {
            ChooseDestination();
        }

        if(larger)
        {
            if(transform.position.x < destination)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
            else
            {
                destinationChosen = false;
            }
        }
        else
        {
            if (transform.position.x > destination)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            else
            {
                destinationChosen = false;
            }
        }

        if (startAttacking)
        {
            startAttacking = false;
            timeUntilAttack = Random.Range(attackTimeMin, attackTimeMax);
        }
        else
        {
            timeUntilAttack -= Time.deltaTime;
            if(timeUntilAttack <= 0f && !isAttacking)
            {
                isAttacking = true;
                Attack();
            }
        }
	}

    void Attack()
    {
        GameObject fire1 = Instantiate(fireball, transform.position + new Vector3(-.1f, .5f, 0), Quaternion.identity);
        GameObject fire2 = Instantiate(fireball, transform.position + new Vector3(-.1f, .5f, 0), Quaternion.identity);
        GameObject fire3 = Instantiate(fireball, transform.position + new Vector3(-.1f, .5f, 0), Quaternion.identity);
        fire1.transform.Rotate(0, 0, 105f + (10 * (multiplyer * -yValue)));
        fire2.transform.Rotate(0, 0, 90f + (10 * (multiplyer * -yValue)));
        fire3.transform.Rotate(0, 0, 75f + (10 * (multiplyer * -yValue)));

        fire1.transform.position += Vector3.forward;
        fire2.transform.position += Vector3.forward;
        fire3.transform.position += Vector3.forward;

        isAttacking = false;
        startAttacking = true;
    }

    void ChooseDestination()
    {
        distance = Random.Range(.5f, 1.5f);
        destinationChosen = true;

        if(distance + transform.position.x > rangeMax)
        {
            destination = transform.position.x - distance;
            larger = false;
        }
        else if(transform.position.x - distance < rangeMin)
        {
            destination = transform.position.x + distance;
            larger = true;
        }
        else
        {
            if(Random.Range(0f, 1f) > .5f)
            {
                destination = transform.position.x + distance;
                larger = true;
            }
            else
            {
                destination = transform.position.x - distance;
                larger = false;
            }
        }
    }
}
