using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

    public int rupee_count = 0;
	public float key_count = 1;
    public int bomb_count = 0;
    public int max_count = 255;
	public string offhand ="";
	public Text offHandInfo;
	// 0 for none, 1 for bomb, 2 for boomer
	int itemIndex = 0;
	string[] items = {"None", "Bomb", "Boomer", "Bow"};
	public bool hasBoomer = false;

	void Update(){
		if (Input.GetKeyDown (KeyCode.B)) {
			itemIndex += 1;
			string item = items [itemIndex % 4];
			if (item.Equals ("Bomb") && bomb_count > 0)
				offhand = "Bomb";
			else if (item.Equals ("Boomer") && hasBoomer)
				offhand = "Boomer";
            else if (item.Equals("Bow") && GetComponent<Bow>().hasBow)
                offhand = "Bow";
            else
				offhand = "None";
			offHandInfo.text = "B:\n" + offhand;
		}
	}

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
