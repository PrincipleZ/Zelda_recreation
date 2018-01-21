using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour {

	public Sprite full_heart;
	public Sprite half_heart;
	public Sprite empty_heart;
	public GameObject heartPrefab;

	private Vector3 heartPositionOffset = new Vector3(30f, 0, 0);
	private GameObject[] hearts;
	Player playerScript;


	void Start(){
		playerScript = GameObject.FindWithTag ("Player").GetComponent<Player>();
		hearts = new GameObject[(playerScript.maxHealth+1)/2]; 

		for (int i = 0; i < hearts.Length; i++){
			
			hearts [i] = (GameObject)Instantiate (heartPrefab, transform);
			hearts [i].transform.position += i * heartPositionOffset;

//			hearts [i].transform.position = heartInitialPosition + i * heartPositionOffset;
		}
		if (playerScript.maxHealth % 2 != 0)
			hearts [hearts.Length - 1].GetComponent<Image> ().sprite = half_heart;
		
	}
	public void change(int health){
		int i = 0;
		while(i < health/2){
			hearts [i].GetComponent<Image> ().sprite = full_heart;
			i++;
		}
		if (health - i*2 == 1){
			hearts [i].GetComponent<Image> ().sprite = half_heart;
			i++;
		} 
		while(i < hearts.Length){
			hearts [i].GetComponent<Image> ().sprite = empty_heart;
			i++;
		}
	}
	public void changeMaxHealth(int health){
		GameObject[] newhearts = new GameObject[health];
		if (health < hearts.Length){
			for (int i = 0; i < health; i++){
				newhearts [i] = hearts [i];
			}
			hearts = newhearts;
		} else{
			for (int i = 0; i < hearts.Length; i++){
				newhearts [i] = hearts [i];
			}
			for (int i = hearts.Length; i < health; i++){
				newhearts[i] = (GameObject)Instantiate (heartPrefab, transform);
				newhearts[i].transform.position += i * heartPositionOffset;
			}
			hearts = newhearts;
		}
	}
}
