using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour {

	public GameObject blackscreen;
	public GameObject gameOverText;
	Image image;
	Color tempColor;
	// Use this for initialization
	void Start () {
		image = blackscreen.GetComponent<Image> ();
		tempColor = image.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		

	}

	void FixedUpdate(){
		if (image.color.a < 1f){
			tempColor.a += 0.02f;
			image.color = tempColor;
		}
		else
			gameOverText.SetActive(true);
	}


}
