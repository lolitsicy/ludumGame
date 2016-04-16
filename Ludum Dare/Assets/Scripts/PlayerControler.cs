using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	public KeyCode moveRightKey = KeyCode.J;
	public KeyCode moveLeftKey = KeyCode.L;
	public KeyCode jumpKey = KeyCode.K;
	public float jumpForce = 10f;
	public float moveForce = 50f;
	public Transform groundCheck;
	private bool grounded = true;
	private int jumpCount = 0;
	private Rigidbody2D bodyBox;
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
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

		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveLeft ();
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			moveRight ();
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			jump ();	
		}
	}

	void moveRight() {
		obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,-10f));
	}

	void moveLeft() {
		obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,10f));
	}

	void jump() {
		jumpCount++;
		//bodyBox.velocity.Set (bodyBox.velocity.x + x, bodyBox.velocity.y + y);
//		bodyBox.velocity.y += y;
		bodyBox.AddForce (new Vector2 (x, y));


	}
		



}
