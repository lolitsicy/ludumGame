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
	public GameObject obj;
	[HideInInspector] public float angle;
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public float x, y, chargeTime, avgAngle;
	[HideInInspector] public Vector2 forceMagNormalize;


	// Use this for initialization
	void Start ()
	{
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
		angle = obj.GetComponent<Transform> ().eulerAngles.magnitude;
		avgAngle = angle;
	}
		
	// Update is called once per frame
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		angle = obj.GetComponent<Transform> ().eulerAngles.magnitude;
		x = jumpForce * Mathf.Cos (angle * Mathf.Deg2Rad);
		y = jumpForce * Mathf.Sin (angle * Mathf.Deg2Rad);

		if (Input.GetKey (moveLeftKey)) {
			moveLeft ();
		}

		if (Input.GetKey (moveRightKey)) {
			moveRight ();
		}

		if (Input.GetKey (jumpKey)) {
			jump ();	
		}
	}

	/**
	 * Called when the player holds down the moveRightKey.
	 * */
	void moveRight() {
		if ((angle > 0 && angle < 180)) {
			if (angle == 0) {
				obj.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, 1f));
			} else {
				obj.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, -1f));

			}
		}		
	}

	/**
	 * Called when the player holds down the moveLeftKey.
	 * */
	void moveLeft() {
		if (angle < 180) {

			obj.GetComponent<Transform> ().Rotate (new Vector3 (0f, 0f, 1f));
		}
	}

	/**
	 * Called when the player holds down the jumpKey.
	 * */
	void jump() {
			
		bodyBox.AddForce (new Vector2(x, y));
	}

}
