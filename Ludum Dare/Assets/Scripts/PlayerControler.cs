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
	private int jumpCount = 0;
	private Rigidbody2D bodyBox;
	public GameObject obj;


	[HideInInspector] public float x, y;
	// Use this for initialization
	void Start () {
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
		bodyBox.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		float angle = obj.GetComponent<Transform>().eulerAngles.magnitude;
		x = jumpForce* Mathf.Cos (angle*Mathf.Deg2Rad);
		y = jumpForce * Mathf.Sin (angle*Mathf.Deg2Rad);

		if (grounded) {
			jumpCount = 0;
		}

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
		obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,-turnSpeed * Time.deltaTime));
	}

	/**
	 * Called when the player holds down the moveLeftKey.
	 * */
	void moveLeft() {
		obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,turnSpeed * Time.deltaTime));
	}

	/**
	 * Called when the player holds down the jumpKey.
	 * */
	void jump() {
		jumpCount++;
		bodyBox.AddForce (new Vector2 (x, y));
	}
}
