using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public KeyCode moveRightKey = KeyCode.RightArrow;
	public KeyCode moveLeftKey = KeyCode.LeftArrow;
	public KeyCode jumpKey = KeyCode.UpArrow;
	public float jumpForce = 10f;
	/** The number of degrees a players head will rotate per second */
	public float turnSpeed = 10f;
	public Transform groundCheck;
	private bool grounded = true;
	private Rigidbody2D bodyBox;
	private Transform viewPoint;
	public GameObject obj;
	[HideInInspector] public float angle;
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public float x, y, jumpMag, jumpCount = 0;
	[HideInInspector] public Vector2 forceMagNormalize;
	public float fullChargeJumpTime = 1f;
	public float fullChargeJumpForce = 15000f;
	public float minChargeJumpForce = 6000f;
	private float chargeTime = 0f;
	public float aerialDriftForce = 10f;
	public float driftGravity = 0.5f;

	// Use this for initialization
	void Start ()
	{
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
		viewPoint = obj.GetComponent<Transform> ();
		angle = viewPoint.eulerAngles.z;

	}
		
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		angle = viewPoint.eulerAngles.magnitude;
		x = Mathf.Cos (angle * Mathf.Deg2Rad);
		y = Mathf.Sin (angle * Mathf.Deg2Rad);

		if (grounded) {
			if (Input.GetKey (moveLeftKey)) {
				moveLeft2 ();
			}

			if (Input.GetKey (moveRightKey)) {
				Debug.Log ("MoveRight");
				moveRight2 ();
			}
			if (Input.GetKey (jumpKey)) {
				Debug.Log ("Charging JUMP");
				chargeJump ();
			}
			if (Input.GetKeyUp (jumpKey) && jumpCount != 1) {
				jumpFromGround ();
			}

			jumpCount = 0;
		} else {
			moveInAir ();
			if (Input.GetKey (moveLeftKey)) {
				moveLeft2 ();
			}

			if (Input.GetKey (moveRightKey)) {
				Debug.Log ("MoveRight");
				moveRight2 ();
			}
			if (Input.GetKey (jumpKey)) {
				Debug.Log ("Charging JUMP");
				chargeJump ();
			}
			if (Input.GetKeyUp (jumpKey) && jumpCount != 1) {
				jumpFromGround ();
			}
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "wall") {
			jumpCount -= 1;
		}
	}

	void moveInAir() {
		bodyBox.AddForce (getViewDirection () * aerialDriftForce);
		bodyBox.gravityScale = driftGravity;
	}

	/**
	 * Returns the current view direction based on user input keys
	 * */
	public Vector2 getViewDirection() {
		float x = 0f;
		float y = 0f;
		if (Input.GetKey (moveLeftKey)) { 
			x -= 1.0f; 
		}
		if (Input.GetKey (moveRightKey)) { 
			x += 1.0f;
		}
		return new Vector2 (x, y);
	}

	/**
	 * Called when the player holds down the moveRightKey.
	 * */
	void moveRight() {
		Debug.Log ("MoveRight: " + viewPoint.eulerAngles);
		if ((angle > 0 && angle < 180)) {
			if (angle == 0) {
				viewPoint.eulerAngles = new Vector3 (0f, 0f, 0f);
			} else {
				viewPoint.Rotate (new Vector3 (0f, 0f, -2f));
			}
		} else {
			viewPoint.eulerAngles = new Vector3 (0f, 0f, 0f);
		}
//		if (angle >= 0) {
//			obj.GetComponent<Transform> ().eulerAngles += new Vector3 (0f, 0f, -1f);
//		} else {
//			obj.GetComponent<Transform> ().eulerAngles = new Vector3 (0f, 0f, 1f);
//		}
	}

	void moveRight2() {
		Debug.Log ("MoveRight: " + viewPoint.eulerAngles);
		if (angle > 0 && angle <= 360) {
			viewPoint.eulerAngles += turnSpeed *(new Vector3 (0f, 0f, -2f));
		} else {
			viewPoint.eulerAngles = new Vector3 (0f, 0f, 0f);
		}
	}


	void moveLeft2() {
		Debug.Log ("MoveLeft: " + viewPoint.eulerAngles);
		if (angle >= 0 && angle < 360) {
			viewPoint.eulerAngles += turnSpeed *(new Vector3 (0f, 0f, 2f));
		} else {
			viewPoint.eulerAngles = new Vector3 (0f, 0f, -180f);
		}
	}

	/**
	 * Called when the player holds down the moveLeftKey.
	 * */
	void moveLeft() {
		if (angle < 180) {
			viewPoint.Rotate (new Vector3 (0f, 0f, 1f));
		} else {
			viewPoint.eulerAngles = new Vector3 (0f, 0f, 180f);
		}
	}

	/**
	 * Called when the player holds down the jumpKey.
	 * */
	void jump() {
			
		bodyBox.AddForce (new Vector2(x, y));
	}

	void jumpFromGround() {
		jumpCount++;
		Debug.Log ((chargeTime / fullChargeJumpTime * (fullChargeJumpForce - minChargeJumpForce)) + minChargeJumpForce);
		jumpMag = (chargeTime / fullChargeJumpTime * (fullChargeJumpForce - minChargeJumpForce)) + minChargeJumpForce;
		bodyBox.AddForce ((new Vector2 (x*jumpMag,y*jumpMag)));
		chargeTime = 0f;
	}

	void chargeJump() {
		if (chargeTime < fullChargeJumpTime) {
			chargeTime += Time.deltaTime;
		}
	}
}
