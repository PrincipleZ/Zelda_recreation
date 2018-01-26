using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPushing : MonoBehaviour {

    public GameObject enabler;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enabler.GetComponent<blockPushEnabler>().canPush == true)
        {
            GetComponent<PushableBlock>().enabled = true;
        }
	}
}
