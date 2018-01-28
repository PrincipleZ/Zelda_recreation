using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : MonoBehaviour {
	public bool switching = false;
	public float cameraSwitchTime = 1f;
	bool transition = false;
	float cameraXOffset = 16f;
	float cameraYOffset = 11f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (switching) {
			GameObject temp = GameObject.Find ("Blue Portal(Clone)");
			if (temp != null) {
				Destroy (temp);
			}
			temp = GameObject.Find ("Orange Portal(Clone)");
			if (temp != null) {
				Destroy (temp);
			}
		}
			
	}
		
	public void switchControl(Vector3 direction){
		if (direction.y == 0) {
			StartCoroutine (cameraMove (transform.position, transform.position + cameraXOffset * direction, cameraSwitchTime));
		}
		if (direction.x == 0){
			StartCoroutine (cameraMove (transform.position, transform.position + cameraYOffset * direction, cameraSwitchTime));

		}

	}

	IEnumerator cameraMove(Vector3 start, Vector3 end, float duration){
		switching = true;
		for (float t = 0f; t < duration; t += Time.deltaTime){
			transform.position = Vector3.Lerp (start, end, t / duration);
			yield return null;
		}
		transform.position = end;
		switching = false;
	}

	public void warpControl(bool up){
		if (!up)
			transform.position = new Vector3 (73.5f, 7.5f, -10f);
		else
			transform.position = new Vector3 (23.5f, 62.5f, -10f);
	}


}
