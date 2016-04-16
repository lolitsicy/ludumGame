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
	// Use this for initialization
	void Start () {
		bodyBox = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
//		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
//		if (grounded) {
//			jumpCount = 0;
//		}
//		if (Input.GetKey (moveLeftKey) && Input.GetKeyDown (moveRightKey)) {
//			return;
//		}
//		if (Input.GetKey (moveRightKey)) {
//			moveRight ();
//		}
//		if (Input.GetKey (moveLeftKey)) {
//			moveLeft ();
//		}
//		if (Input.GetKey (jumpKey) && jumpCount < 2) {
//			jump ();
//		}
		float angle = obj.GetComponent<Transform>().eulerAngles.magnitude;
		float x = jumpForce* Mathf.Cos (angle*Mathf.Deg2Rad);
		float y = jumpForce * Mathf.Sin (angle*Mathf.Deg2Rad);

		if (Input.GetKey (KeyCode.LeftArrow)) obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,10f));
//			bodyBox.AddTorque (4, ForceMode2D.Force);

		if (Input.GetKey (KeyCode.RightArrow)) obj.GetComponent<Transform> ().Rotate (new Vector3 (0f,0f,-10f));

		if (Input.GetKey (KeyCode.UpArrow)) {
			
			bodyBox.AddForce (new Vector2 (x, y));

		}
	}

	void moveRight() {
		bodyBox.AddForce (new Vector2 (moveForce, 0f));
	}

	void moveLeft() {
		bodyBox.AddForce (new Vector2 (-moveForce, 0f));
	}

	void jump() {
		jumpCount++;
		bodyBox.AddForce (new Vector2 (0f, jumpForce));
	}

}
