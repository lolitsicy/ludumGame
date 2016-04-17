using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public KeyCode moveRightKey = KeyCode.RightArrow;
	public KeyCode moveLeftKey = KeyCode.LeftArrow;
	public KeyCode jumpKey = KeyCode.Space;
	/** The number of degrees a players head will rotate per second */
	public float turnSpeed = 4f;
	public Transform groundCheck;
	public GameObject obj;
	private bool grounded = true;
	private Rigidbody2D bodyBox;
	private Transform viewPoint;
	[HideInInspector] public float x, y, jumpMag, jumpCount = 0;
	/** Time in seconds for the charge jump to be fully charged */
	public float fullChargeJumpTime = 1f;
	public float fullChargeJumpForce = 15000f;
	public float minChargeJumpForce = 6000f;
	private float chargeTime = 0f;
	private int score = 0;

	/** 
	 * When a player eats the food call this function
	 * */
	void addScore(int amount) {
		score++;
	}

	public int getScore() {
		return score;
	}

	// Use this for initialization
	void Start ()
	{
		score = 0;
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
		viewPoint = obj.GetComponent<Transform> ();
	}
		
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		float angle = viewPoint.eulerAngles.z;
		x = Mathf.Cos (angle * Mathf.Deg2Rad);
		y = Mathf.Sin (angle * Mathf.Deg2Rad);

		if (grounded) {
			if (Input.GetKey (moveLeftKey)) {
				moveLeft ();
			}

			if (Input.GetKey (moveRightKey)) {
				Debug.Log ("MoveRight");
				moveRight();
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
		if (coll.gameObject.tag == "food") {
			addScore (1);
			Destroy (coll.gameObject);
		}

	}
		
	void moveRight() {
		viewPoint.eulerAngles += turnSpeed *(new Vector3 (0f, 0f, -turnSpeed));
	}

	void moveLeft() {
		viewPoint.eulerAngles += turnSpeed *(new Vector3 (0f, 0f, turnSpeed));
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
