using UnityEngine;
using System.Collections;

public class MushroomBeh : MonoBehaviour {

	Vector2 originalVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col) {

		originalVelocity = col.gameObject.rigidbody2D.velocity;
		originalVelocity = originalVelocity * -1;

		if (Input.GetButton ("Fire1") && originalVelocity.y < 13f) {

			originalVelocity.y += 2f;

			if (originalVelocity.y > 13f)
			{
				originalVelocity.y = 13f;
			}
		}

		col.gameObject.rigidbody2D.velocity = originalVelocity;
	}
}
