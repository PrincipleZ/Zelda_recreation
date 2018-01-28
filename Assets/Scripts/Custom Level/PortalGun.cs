using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour {

	public GameObject bluePortal;
	public GameObject orangePortal;
	public GameObject blueProjectile;
	public GameObject orangeProjectile;

	GameObject projectile;
	SwordDirection swordScript;

	// Use this for initialization
	void Start () {
		swordScript = GetComponent<SwordDirection> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			Vector3 dir = Vector3.zero;
            GetComponent<playerSounds>().PortalShot();
			switch (swordScript.directionFacingNESW) {
			case 0:
				dir = new Vector3 (0, 1, 0);
				break;
			case 1:
				dir = new Vector3 (1, 0, 0);
				break;
			case 2:
				dir = new Vector3 (0, -1, 0);
				break;
			case 3:
				dir = new Vector3 (-1, 0, 0);
				break;
			}
			projectile = Instantiate (orangeProjectile, transform.position + dir, Quaternion.identity);
			projectile.GetComponent<OrangeProjectileScript> ().direction = dir;

		}
		if (Input.GetButtonDown ("Fire2")) {
			Vector3 dir = Vector3.zero;
            GetComponent<playerSounds>().PortalShot();
            switch (swordScript.directionFacingNESW) {
			case 0:
				dir = new Vector3 (0, 1, 0);
				break;
			case 1:
				dir = new Vector3 (1, 0, 0);
				break;
			case 2:
				dir = new Vector3 (0, -1, 0);
				break;
			case 3:
				dir = new Vector3 (-1, 0, 0);
				break;
			}
			projectile = Instantiate (blueProjectile, transform.position + dir, Quaternion.identity);
			projectile.GetComponent<BlueProjectileScript> ().direction = dir;

		}

	}
}
