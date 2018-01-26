using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{

    public float movement_speed = 4;
	public float link_y_offset  = 0f;
    public bool isDoingAction = false;
	Player playerScript;
	float old_horizontal_input = 0;
	float old_vertical_input = 0;
	public Vector2 direction = Vector2.zero;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		playerScript = GameObject.FindWithTag ("Player").GetComponent<Player> ();
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 current_input = GetInput();
		if (playerScript.movement && !playerScript.dead) {
			if (grid_adjust (current_input))
				rb.velocity = Vector3.zero;
			else
				rb.velocity = movement_speed * current_input;
		}
    }

    Vector2 GetInput()
    {

        if (isDoingAction)
        {
            return new Vector2(0, 0);
        }
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");
		if (horizontal_input != 0 && vertical_input != 0) {
			if (horizontal_input != old_horizontal_input || vertical_input != old_vertical_input) {
				if (horizontal_input != old_horizontal_input) {
					if (horizontal_input != 0) {
						direction = new Vector2 (horizontal_input, 0);
					} 
					old_horizontal_input = horizontal_input;
				} else if (vertical_input != old_vertical_input) {
					if (vertical_input != 0) {
						direction = new Vector2 (0, vertical_input);
					}
					old_vertical_input = vertical_input;
				}
			}
		}else if (horizontal_input == 0 && vertical_input == 0) {
			direction = Vector2.zero;
			old_horizontal_input = 0;
			old_vertical_input = 0;
		} else if (horizontal_input == 0){
			direction = new Vector2 (0, vertical_input);
			old_horizontal_input = horizontal_input;
			old_vertical_input = vertical_input;
		} else if (vertical_input == 0){
			direction = new Vector2 (horizontal_input, 0);
			old_vertical_input = vertical_input;
			old_horizontal_input = horizontal_input;
		} 
//        if (Mathf.Abs(horizontal_input) > 0.0f)
//        {
//            vertical_input = 0.0f;
//        }
		return direction;
    }

	// Set the value of not changing axis to nearst 0.5. Need to add transition animation
	bool grid_adjust(Vector2 current_input){
		Vector2 prevPos = transform.position;
		if (Mathf.Abs(current_input.x) > 0f){
			float newY = Mathf.Round (prevPos.y * 2) / 2f + link_y_offset;
			Debug.Log (newY);
			if (prevPos.y == newY)
				return false;
			transform.position = new Vector2 (prevPos.x, newY);
			return true;
		} else if (Mathf.Abs(current_input.y) > 0f){
			float newX = Mathf.Round (prevPos.x * 2) / 2f;
			if (prevPos.x == newX)
				return false;
			transform.position = new Vector2 (newX, prevPos.y);
			return true;

		}
		return false;

	}
}

