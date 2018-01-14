using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int rupee_count = 0;
	public float key_count = 1;
    public int bomb_count = 0;
    public int max_count = 255;

    public void AddRupees(int num_rupees)
    {
        rupee_count += num_rupees;
        if (rupee_count > max_count)
            rupee_count = max_count;
    }

	public void AddKeys(int num_keys){
		key_count += num_keys;

        if (key_count > max_count)
            key_count = max_count;
	}

    public void AddBombs(int num_bombs)
    {
        bomb_count += num_bombs;

        if (bomb_count > max_count)
            bomb_count = max_count;
    }

    public int GetRupees()
    {
        return rupee_count;
    }

	public float GetKeys(){
		return key_count;
	}

    public int GetBombs()
    {
        return bomb_count;
    }
}
