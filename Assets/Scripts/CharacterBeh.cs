using UnityEngine;
using System.Collections;

public class CharacterBeh : MonoBehaviour {

	int moveDir = 0;
	public float speed;
	Vector2 playerVelocity;
	float jumpHeight = 0.1f;
	public float velocityY;
	public bool Grounded;
	Vector2 Origin;
	LayerMask groundLayer = 1<<9;

	public bool canClimb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Origin = new Vector2 (transform.position.x, transform.position.y - 0.5f);
		velocityY = rigidbody2D.velocity.y;
		float input = Input.GetAxisRaw("Horizontal");

		if (input > 0) {

			moveDir = 1;
		} else if (input < 0) {
		
			moveDir = -1;
		} else {

			moveDir = 0;
		}

		Jump ();
		Walk ();
	}

	void Walk () {

		if (moveDir > 0) {

			playerVelocity = new Vector2 ((1 * speed/Time.deltaTime), velocityY);
			rigidbody2D.velocity = playerVelocity;
		} else if (moveDir < 0) {

			playerVelocity = new Vector2 ((-1 * speed/Time.deltaTime), velocityY);
			rigidbody2D.velocity = playerVelocity;
		} else {

			playerVelocity = new Vector2 (0, velocityY);
			rigidbody2D.velocity = playerVelocity;
		}
	}

	void Jump () {


		if (Input.GetButtonDown ("Fire1")) {
			GroundCheck();
			if (Grounded == true){
				velocityY = (1 * jumpHeight/Time.deltaTime);

				if (velocityY > 9f)
				{
					velocityY = 9f;
				}

				rigidbody2D.AddForce(new Vector2(0,velocityY), ForceMode2D.Impulse);

			}
		}
	}

	void GroundCheck (){

		if (Physics2D.Raycast (Origin, -Vector2.up, 0.1f, groundLayer))
			Grounded = true;
		else 
			Grounded = false;
	}

	void Climb (){

	}
	void OnTriggerStay2D (Collider2D col){

		canClimb = true;
	}

	void OnTriggerExit2D (Collider2D col){

		canClimb = false;
	}
}
