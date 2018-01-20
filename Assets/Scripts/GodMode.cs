using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour {

    public bool GodModeActive = false;

	// Update is called once per frame
	void Update () {

        if(GodModeActive)
            GetComponent<Player>().invincible = true;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!GodModeActive)
            {
                GetComponent<Inventory>().AddRupees(GetComponent<Inventory>().max_count);
                GetComponent<Inventory>().AddKeys(GetComponent<Inventory>().max_count);
                GetComponent<Inventory>().AddBombs(GetComponent<Inventory>().max_count);
                GetComponent<Player>().invincible = true;
                GodModeActive = true;
            }
            else
            {
                GetComponent<Player>().invincible = false;
                GodModeActive = false;
            }
        }
	}
}
