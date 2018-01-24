using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {
	bool countDownOver = false;
	public GameObject smokePrefab;
	GameObject[] breakable_walls;
    public AudioClip explosion;
	// Use this for initialization
	void Start () {
		StartCoroutine (CountDown ());
		breakable_walls = GameObject.FindGameObjectsWithTag ("breakable wall");


	}
	
	// Update is called once per frame
	void Update () {

		if (countDownOver){
            AudioSource.PlayClipAtPoint(explosion, Camera.main.transform.position);

            for (int i = 0; i < 8; i++){
				RaycastHit temp;
				if (Physics.Raycast(transform.position, Quaternion.Euler(0, 0, i * 45) * Vector3.up, out temp, 1.5f)){

					if (temp.collider.gameObject.tag == "enemy") {
						GameObject tempSmoke = (GameObject)Instantiate (smokePrefab, temp.transform.position, Quaternion.identity);
						Destroy (temp.collider.gameObject);
						tempSmoke.GetComponent<SmokeDisappear> ().disappear ();
					}
					else if (temp.collider.gameObject.tag == "breakable wall"){
						for (int j = 0; j < breakable_walls.Length; j++) {
							if (breakable_walls [j] != null){
								GameObject tempSmoke = (GameObject)Instantiate (smokePrefab, breakable_walls [j].transform.position, Quaternion.identity);
								Destroy (breakable_walls [j]);
								tempSmoke.GetComponent<SmokeDisappear> ().disappear ();
							}
						}
					}
				}
					
			}
			GetComponent<SpriteRenderer> ().enabled = false;
			GameObject smoke = (GameObject)Instantiate (smokePrefab, transform.position, Quaternion.identity);
			smoke.GetComponent<SmokeDisappear> ().disappear ();
			Destroy (gameObject);


		}
	}

	IEnumerator CountDown(){
		yield return new WaitForSeconds (1f);
		countDownOver = true;
	}


}
