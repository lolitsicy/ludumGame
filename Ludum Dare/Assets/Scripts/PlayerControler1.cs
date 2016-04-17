using UnityEngine;
using System.Collections;
/**
 * The Implementation of player control for PlayTest2.
 * */
public class PlayerControler1 : MonoBehaviour {
	public KeyCode moveRightKey = KeyCode.RightArrow;
	public KeyCode moveLeftKey = KeyCode.LeftArrow;
	public KeyCode moveUpKey = KeyCode.UpArrow;
	public KeyCode moveDownKey = KeyCode.DownArrow;
	public KeyCode jumpKey = KeyCode.Space;
	/** The magnitude of the force that the character will move horizontally when on the ground */
	public float groundedMoveForce = 100f;
	/** The magnitude of the force that the character will drift when moving in the air */
	public float aerialDriftForce = 10f;
	public float driftGravity = 1.0f;
	public float maxSpeed = 1.0f;

	public float fullChargeJumpTime = 3.0f;
	public float fullChargeJumpForce = 2000f;
	public float minChargeJumpForce = 200f;
	public float aerialJumpForce = 1000f;
	public Transform groundCheck;
	private bool grounded = true;
	private int jumpCount = 0;
	private Rigidbody2D bodyBox;
	private float chargeTime = 0f;

	[HideInInspector] public float x, y;
	// Use this for initialization
	void Start () {
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
		bodyBox.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = isGrounded ();
		EnforceMaxSpeed ();
		if (grounded) {
			jumpCount = 0;
			if (Input.GetKey (moveLeftKey)) {
				moveLeftOnGround ();
			}
			if (Input.GetKey (moveRightKey)) {
				moveRightOnGround ();
			}
			if (Input.GetKey (jumpKey)) {
				Debug.Log ("Charging JUMP");
				chargeJump ();
			}
			if (Input.GetKeyUp (jumpKey)) {
				jumpFromGround ();
			}
		} else {
			if (Input.GetKey (moveLeftKey) || Input.GetKey(moveRightKey) || Input.GetKey(moveUpKey) || Input.GetKey(moveDownKey)) {
				moveInAir ();
			}
			if (Input.GetKeyUp (jumpKey)) {
				Debug.Log ("Aerial Jump");
				aerialJump ();
			}
		}
	}

	/**
	 * Return true if there are objects on the ground layer overlapping the players groundCheck transform point.
	 * */
	public bool isGrounded() {
		return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
	}
	/**
	 * Returns the current view direction based on user input keys
	 * */
	public Vector2 getViewDirection() {
		float x = 0f;
		float y = 0f;
		if (Input.GetKey (moveUpKey)) { 
			y += 1.0f; 
		}
		if (Input.GetKey (moveDownKey)) { 
			y -= 1.0f; 
		}
		if (Input.GetKey (moveLeftKey)) { 
			x -= 1.0f; 
		}
		if (Input.GetKey (moveRightKey)) { 
			x += 1.0f;
		}
		return new Vector2 (x, y);
	}

	void EnforceMaxSpeed() {
		if (bodyBox.velocity.magnitude > maxSpeed) {
			bodyBox.velocity = bodyBox.velocity.normalized * maxSpeed;
			if (bodyBox.velocity.magnitude > maxSpeed) {
				Debug.Log ("lol wtf");
			}
		}
	}

	/**
	 * Moves the player to the right. Should be called when they are grounded.
	 * */
	void moveRightOnGround() {
		Debug.Log ("MOVING RIGHT");
		bodyBox.AddForce (new Vector2 (groundedMoveForce, 0f));
	}

	void moveInAir() {
		Vector2 AerialDrift = getViewDirection ();
		if (AerialDrift.y > 0) {
			AerialDrift.y = 0f;
		}
		bodyBox.AddForce (AerialDrift * aerialDriftForce);
		bodyBox.gravityScale = driftGravity;
	}

	/** Applying an aerial jump of half force of full jump */
	void aerialJump() {
		bodyBox.AddForce (getViewDirection() * (0.5f * fullChargeJumpForce));
	}

	/**
	 * Moves the player to the left. Should be called when they are grounded.
	 * */
	void moveLeftOnGround() {
		Debug.Log ("MOVING LEFT");
		bodyBox.AddForce (new Vector2 (-1 * groundedMoveForce, 0f));
		bodyBox.gravityScale = 1;
	}

	/**
	 * Causes the player to jump. Should be called when they are grounded.
	 * */
	void jumpFromGround() {
		jumpCount++;
		Debug.Log ((chargeTime / fullChargeJumpTime * (fullChargeJumpForce - minChargeJumpForce)) + minChargeJumpForce);
		bodyBox.AddForce (new Vector2 (0f, (chargeTime / fullChargeJumpTime * (fullChargeJumpForce - minChargeJumpForce)) + minChargeJumpForce));
		chargeTime = 0f;
	}

	void chargeJump() {
		if (chargeTime < fullChargeJumpTime) {
			chargeTime += Time.deltaTime;
		}
	}
}
