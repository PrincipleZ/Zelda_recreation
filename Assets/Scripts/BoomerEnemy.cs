using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerEnemy : MonoBehaviour
{
    public bool boomerCanary = false;
    public bool deleteObject = false;

    Vector3 lastVelocity;
    bool shot;
    public bool back;
    Vector3 direction;
    Rigidbody rb;
    Animator animator;
    Transform parentTransform;
    Transform trans;
    public float force = 2f;
    public float backSpeed = 3f;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
//        Debug.Log(rb);
        animator = GetComponent<Animator>();
        shot = false;
        back = false;
        trans = GetComponent<Transform>();
        direction = Vector3.zero;
        parentTransform = transform;
    }


    public void shoot(Vector3 dir, Transform pTransform)
    {
        gameObject.SetActive(true);
        direction = dir;
        parentTransform = pTransform;

        rb.velocity = force * direction;
        shot = true;

    }

    void FixedUpdate()
    {
        if (shot)
        {
            if (!back)
                rb.velocity -= direction / 10;
            //		Debug.Log (rb.velocity);
            if (rb.velocity == Vector3.zero)
            {
                back = true;
            }
            if (back)
            {
                rb.velocity = backSpeed * (parentTransform.position - trans.position).normalized;
                if (Vector3.Distance(trans.position, parentTransform.position) < 0.2f)
                {
                    boomerCanary = true;
                }
            }
        }
        if (deleteObject)
        {
            Destroy(this.gameObject);
        }
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //play hit wall effect
        if (!back)
        {
            back = true;
            rb.velocity = Vector3.zero;
        }
        if(collision.gameObject.GetComponent<Player>())
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
