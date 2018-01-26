using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int currentHealth = 6;
	public int maxHealth = 6;
	public float flashTime = 1f;
	public float force = 750f;
	public float knockBackTime = 0.3f;
	private Animator anim;
	Rigidbody rb;
	public bool invincible = false;
	public bool movement = true;
	public bool dead = false;
	public GameObject GameOverScreen;
	ChangeHealth changeHealthScript;
	RoomSwitch cameraScript;
	public GameObject boomerPrefab;
	public GameObject bombPrefab;
	SwordDirection swordScript;
	GameObject boomer;
	GameObject bomb;
	Inventory inventoryScript;
	Bow bowScript;
    boomerang boomerScript;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		currentHealth = maxHealth;
		changeHealthScript = GameObject.Find ("HeartManager").GetComponent<ChangeHealth> ();
		anim = GetComponent<Animator> ();
		cameraScript = GameObject.FindWithTag ("MainCamera").GetComponent<RoomSwitch> ();
		swordScript = GetComponent<SwordDirection> ();
		inventoryScript = GetComponent<Inventory> ();
		bowScript = GetComponent<Bow> ();
        boomerScript = GetComponent<boomerang>();
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetButtonDown("Fire2")){
			Vector3 dir = Vector3.zero;
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
			if (inventoryScript.offhand == "Boomer" && boomer == null) {
				StartCoroutine (ShootBoomer ());

				boomer = (GameObject)Instantiate (boomerPrefab, transform.position + dir, Quaternion.identity);
				Debug.Log (boomer.transform.position);
				boomer.GetComponent<boomerang> ().shoot (dir, transform);
			}
			else if (inventoryScript.offhand == "Bomb" && bomb == null){
                GetComponent<playerSounds>().DropBomb();
				inventoryScript.bomb_count -= 1;
				bomb = (GameObject)Instantiate (bombPrefab, transform.position + dir, Quaternion.identity);

			}
            else if (inventoryScript.offhand == "Bow" && bowScript.hasBow)
            {
                
                bowScript.shootArrow();

            }
        }
	}

	void FixedUpdate(){

	}


	void OnCollisionStay(Collision collision){

		if (collision.gameObject.GetComponent<EnemyDamage>())
        {
            OnHit(collision);
        }
	}

	void OnTriggerEnter(Collider collider){
		GameObject temp = collider.gameObject;
		if (temp.name == "bow"){
			bowScript.hasBow = true;
            GetComponent<playerSounds>().GotNewWeapon();

            Destroy(collider.gameObject);
		}
		else if (temp.name == "LADDER_UP" || temp.name == "LADDER_DOWN"){
			if (temp.name == "LADDER_UP") {
				cameraScript.warpControl (true);
				transform.position = new Vector3 (23f, 60f, 0);
			}
			else{
				cameraScript.warpControl (false);
				transform.position = new Vector3 (69f, 9.6f, 0);
			}
		}

		else if (temp.CompareTag("entrance") && !cameraScript.switching && movement) {
			Vector3 dir = Vector3.zero;
			switch (temp.name){
			// switch to right
			case "Tile_R_EN":
				dir = new Vector3 (1, 0, 0);
				break;
			case "Tile_L_EN":
				dir = new Vector3 (-1, 0, 0);			
				break;
			case "Tile_UPEN":
				dir = new Vector3 (0, 1, 0);			
				break;
			case "Tile_DNEN":
				dir = new Vector3 (0, -1, 0);			
				break;

			}
			cameraScript.switchControl (dir);
			StartCoroutine (waitForCamera(dir));

		}

        else if (temp.CompareTag("boomerWeapon"))
        {
            inventoryScript.hasBoomer = true;
            GetComponent<playerSounds>().GotNewWeapon2();

            Destroy(collider.gameObject);
        }
	}
		
	public void OnHit(Collision collision){
		if (!invincible && !dead) {
            GetComponent<playerSounds>().GotHit();
			currentHealth -= collision.gameObject.GetComponent<EnemyDamage>().damageAmount;
			changeHealthScript.change (currentHealth);
			if (currentHealth == 0) {
				movement = false;
				Die ();
			}
			else{
				Debug.Log (currentHealth);
				if (collision.gameObject.GetComponent<HandMovement> () == null) {
					StartCoroutine (Flash ());
					StartCoroutine (KnockBack (collision));
				}
			}

		}
	}

    public void heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        changeHealthScript.change(currentHealth);
    }

	IEnumerator waitForCamera(Vector3 direction){
		Vector3 start = transform.position;
		movement = false;

		for (float t = 0f; t < cameraScript.cameraSwitchTime; t += Time.deltaTime){
			anim.SetFloat ("horizontal_input", direction.x);
			anim.SetFloat ("vertical_input", direction.y);
			rb.velocity = direction * 1.3f;
			yield return null;
		}
		Debug.Log (transform.position);
		rb.velocity = Vector3.zero;

		movement = true;
	}

	IEnumerator Flash(){
//		anim.SetTrigger ("Flash");
		invincible = true;
		Physics.IgnoreLayerCollision (9, 12, true);
		yield return new WaitForSeconds (flashTime);
		invincible = false;
		Physics.IgnoreLayerCollision (9, 12, false);
	}

	IEnumerator KnockBack(Collision collision){
		movement = false;
		rb.AddForce(force * collision.contacts [0].normal);
		Debug.Log (force * collision.contacts [0].normal);
		yield return new WaitForSeconds (knockBackTime);
		movement = true;
	}

	IEnumerator ShootBoomer(){
		rb.velocity = Vector3.zero;
		movement = false;
		yield return new WaitForSeconds (0.2f);
		movement = true;

	}

	void Die(){
        GetComponent<playerSounds>().DeathAudio();
		dead = true;
		rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
		anim.SetBool ("Dead", true);
		anim.speed = 1;
		StartCoroutine (showBlackScreen ());
	}

	IEnumerator showBlackScreen(){
		yield return new WaitForSeconds (3f);
		GameOverScreen.SetActive (true);
	}
}
