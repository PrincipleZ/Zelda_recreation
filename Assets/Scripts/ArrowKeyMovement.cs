using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{

    public float movement_speed = 4;
	public float link_y_offset  = 0.05f;
	Player playerScript;

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
			grid_adjust (current_input);
			rb.velocity = movement_speed * current_input;
		}
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }

	// Set the value of not changing axis to nearst 0.5. Need to add transition animation
	void grid_adjust(Vector2 current_input){
		Vector2 prevPos = transform.position;
		if (Mathf.Abs(current_input.x) > 0f){
			float newY = Mathf.Round (prevPos.y * 2) / 2f;
			transform.position = new Vector2 (prevPos.x, prevPos.y + (newY - prevPos.y)/2 + link_y_offset);
		} else if (Mathf.Abs(current_input.y) > 0f){
			float newX = Mathf.Round (prevPos.x * 2) / 2f;
			transform.position = new Vector2 (prevPos.x + (newX - prevPos.x)/2, prevPos.y);
		}
	}
}

