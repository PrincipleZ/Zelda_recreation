using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{	
		GetHurt temp = other.gameObject.GetComponent<GetHurt> ();
		if (temp == null)
			temp = other.gameObject.GetComponentInParent<GetHurt> ();
        if (other.name == "AquamentusBody")
        {
            if(this.tag == "sword")
            {
                temp.OnHit(GetComponent<Collider>());
            }
        } 
		else if (temp != null)
			temp.OnHit (GetComponent<Collider>());
 
	}
}
