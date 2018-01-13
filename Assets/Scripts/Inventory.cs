using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int rupee_count = 0;
	public float key_count = 1;

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
    }

	public void AddKeys(int num_keys){
		key_count += num_keys;
	}

    public int GetRupees()
    {
        return rupee_count;
    }

	public float GetKeys(){
		return key_count;
	}
}
