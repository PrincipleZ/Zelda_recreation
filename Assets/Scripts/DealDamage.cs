using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{	
		Debug.Log (other.gameObject.name);
		GetHurt temp = other.gameObject.GetComponent<GetHurt> ();
		if (temp == null)
			temp = other.gameObject.GetComponentInParent<GetHurt> ();
		if (temp != null)
			temp.OnHit (GetComponent<Collider>());
 
	}
}
