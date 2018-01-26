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

		if (countDownOver) {
			AudioSource.PlayClipAtPoint (explosion, Camera.main.transform.position);

			Collider[] hits;
			hits = Physics.OverlapSphere (transform.position, 1.7f);
			Debug.Log (hits.Length);
			if (hits.Length > 0) {
				for (int i = 0; i < hits.Length; i++) {
					if (hits[i].gameObject.tag == "enemy") {
						GameObject tempSmoke = (GameObject)Instantiate (smokePrefab, hits [i].gameObject.transform.position, Quaternion.identity);
						if (hits [i].gameObject.GetComponent<AquamentusScript> () == null) {
							Destroy (hits [i].gameObject);
						}else{
							hits [i].gameObject.GetComponent<EnemyHealth> ().currentHealth -= 4;
						}
						tempSmoke.GetComponent<SmokeDisappear> ().disappear ();
					} else if (hits[i].gameObject.tag == "breakable wall") {
						for (int j = 0; j < breakable_walls.Length; j++) {
							if (breakable_walls [j] != null) {
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
