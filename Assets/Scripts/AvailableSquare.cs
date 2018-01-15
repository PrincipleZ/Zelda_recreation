using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableSquare : MonoBehaviour {

    public bool isAvailable = true;
    public bool directionHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall" || other.tag == "obstacle" || other.tag == "door(wall)")
        {
            isAvailable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "wall" || other.tag == "obstacle" || other.tag == "door(wall)")
        {
            isAvailable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wall" || other.tag == "obstacle" || other.tag == "door(wall)")
        {
            isAvailable = true;
        }
    }
}
