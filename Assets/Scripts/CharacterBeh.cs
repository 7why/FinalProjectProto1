using UnityEngine;
using System.Collections;

public class CharacterBeh : MonoBehaviour {

	int moveDir = 0;
	public float speed;
	public float speedMax;
	public float speedMin;
	Vector2 playerVelocity;
	Vector2 playerVelocityY;
	float jumpHeight = 0.1f;
	public float velocityY;
	public bool Grounded;
	Vector2 Origin;
	LayerMask groundLayer = 1<<9;
	float delay;

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

		Climb ();
		Jump ();
		Walk ();
	}

	void Walk () {

		if (moveDir > 0) {

			if (playerVelocity.x < 0){

				if (speed > speedMin)
				{
					speed -= Time.deltaTime / 10;
				}
				playerVelocity = new Vector2(playerVelocity.x + Time.deltaTime*10, velocityY);
				rigidbody2D.velocity = playerVelocity;
			}

			if (playerVelocity.x > -0.2f) {

				if (speed < speedMax)
				{
					speed += Time.deltaTime / 10;
				}
				playerVelocity = new Vector2 ((1 * speed / Time.deltaTime), velocityY);
				rigidbody2D.velocity = playerVelocity;
			}
		} else if (moveDir < 0) {

			if (playerVelocity.x > 0){

				if (speed > speedMin)
				{
					speed -= Time.deltaTime / 10;
				}
				playerVelocity = new Vector2(playerVelocity.x - Time.deltaTime*10, velocityY);
				rigidbody2D.velocity = playerVelocity;
			}
			if (playerVelocity.x < 0.2f) {

				if (speed < speedMax)
				{
					speed += Time.deltaTime / 10;
				}
				playerVelocity = new Vector2 ((-1 * speed / Time.deltaTime), velocityY);
				rigidbody2D.velocity = playerVelocity;
			}
		} else if (moveDir == 0 && playerVelocity.x > 0f) {

			if (speed > speedMin)
			{
				speed -= Time.deltaTime / 10;
			}
			playerVelocity = new Vector2(playerVelocity.x - Time.deltaTime*3, velocityY);
			rigidbody2D.velocity = playerVelocity;
		} else if (moveDir == 0 && playerVelocity.x < 0f) {
		
			if (speed > speedMin)
			{
				speed -= Time.deltaTime / 10;
			}
			playerVelocity = new Vector2(playerVelocity.x + Time.deltaTime*3, velocityY);
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
		else if (canClimb == true)
			Grounded = true;
		else
			Grounded = false;
	}

	void Climb (){

		if (canClimb == true) {

			float PlayerGravity = 0f;
			float vertical = Input.GetAxisRaw ("Vertical");
			rigidbody2D.gravityScale = PlayerGravity;

			if (vertical > 0) {

				transform.Translate(new Vector3(0, ((1*0.1f/Time.deltaTime)/50), 0));
			}
			if (vertical < 0) {

					transform.Translate(new Vector3(0, ((-1*0.1f/Time.deltaTime)/50), 0));
			} else {
				velocityY = 0f;
				playerVelocityY = new Vector2 (playerVelocity.x, 0f);
				rigidbody2D.velocity = playerVelocityY;
			}
		} else {

			float PlayerGravity = 1f;
			rigidbody2D.gravityScale = PlayerGravity;
		}
	}

	void OnTriggerStay2D (Collider2D col){
		if (col.transform.CompareTag ("Climbable")) {
			canClimb = true;
		}
	}

	void OnTriggerExit2D (Collider2D col){

		canClimb = false;
	}
}
