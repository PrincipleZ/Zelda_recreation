using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableSquare : MonoBehaviour {

    public bool isAvailable = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall" || other.tag == "obstacle" || other.tag == "entrance")
        {
            isAvailable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "wall" || other.tag == "obstacle" || other.tag == "entrance")
        {
            isAvailable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall" || other.tag == "obstacle" || other.tag == "entrance")
        {
            isAvailable = true;
        }
    }
}
