using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour {

    public GameObject heart;
    public GameObject rupee;
    public GameObject rupeeB;
    public GameObject bombPrefab;

    public void DropChance()
    {
        if(GetComponent<GoriyaMovement>() != null && Random.Range(0f, 1f) > .5f)
        {
            float item = Random.Range(0f, 1f);
            if (item < .25f)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }
            else if (item < .50f)
            {
                Instantiate(rupee, transform.position, Quaternion.identity);
            }
            else if(item < .75f)
            {
                Instantiate(rupeeB, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bombPrefab, transform.position, Quaternion.identity);
            }
        }
        else if(Random.Range(0f, 1f) > .5f)
        {
            float item = Random.Range(0f, 1f);
            if(item < .33f)
            {
                Instantiate(heart, transform.position, Quaternion.identity);
            }
            else if (item < .66f)
            {
                Instantiate(rupee, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(rupeeB, transform.position, Quaternion.identity);
            }
        }
    }
}
