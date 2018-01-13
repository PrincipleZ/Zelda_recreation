﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    public GameObject arrow;
    public float arrowSpeed = 10;
    
    int NESWdirection;

    // Update is called once per frame
    void Update () {
        NESWdirection = this.GetComponent<SwordDirection>().directionFacingNESW;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(GetComponent<Inventory>().GetRupees() > 0)
            {
                GetComponent<Inventory>().AddRupees(-1);
                GameObject currentArrow = Instantiate(arrow);

                if (NESWdirection == 0)
                {
                    currentArrow.transform.position = transform.position + new Vector3(0, 1f, 0);
                    currentArrow.GetComponent<Rigidbody>().velocity = new Vector3(0, arrowSpeed, 0);
                }
                else if (NESWdirection == 1)
                {
                    currentArrow.transform.position = transform.position + new Vector3(1f, 0, 0);
                    currentArrow.transform.Rotate(new Vector3(0, 0, 270f));
                    currentArrow.GetComponent<Rigidbody>().velocity = new Vector3(arrowSpeed, 0, 0);
                }
                else if (NESWdirection == 2)
                {
                    currentArrow.transform.position = transform.position + new Vector3(0, -1f, 0);
                    currentArrow.transform.Rotate(new Vector3(0, 0, 180f));
                    currentArrow.GetComponent<Rigidbody>().velocity = new Vector3(0, -arrowSpeed, 0);
                }
                else
                {
                    currentArrow.transform.position = transform.position + new Vector3(-1f, 0, 0);
                    currentArrow.transform.Rotate(new Vector3(0, 0, 90f));
                    currentArrow.GetComponent<Rigidbody>().velocity = new Vector3(-arrowSpeed, 0, 0);
                }
            }

        }
    }
}
