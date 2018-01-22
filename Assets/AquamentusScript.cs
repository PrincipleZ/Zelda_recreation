using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquamentusScript : MonoBehaviour {

    public float moveSpeed = 1f;
    public GameObject fireball;
    public float attackTimeMin = 1.5f;
    public float attackTimeMax = 5f;

    float rangeMin;
    float rangeMax;

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
        fire1.transform.Rotate(0, 0, 80f);
        fire2.transform.Rotate(0, 0, 90f);
        fire3.transform.Rotate(0, 0, 100f);

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
