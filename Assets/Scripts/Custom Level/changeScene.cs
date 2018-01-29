using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScene : MonoBehaviour {

	public AudioSource music;

	bool playing = true;

	public void ChangeSceneTo(int sceneNum){
		Application.LoadLevel (sceneNum);
	}

	public void muteMusic(){
		if (playing) {
			playing = false;
			music.Stop ();
		} else {
			playing = true;
			music.Play ();
		}
	}
}
