using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public KeyCode moveRightKey = KeyCode.RightArrow;
	public KeyCode moveLeftKey = KeyCode.LeftArrow;
	public KeyCode jumpKey = KeyCode.Space;
	/** The number of degrees a players head will rotate per second */
	public float turnSpeed = 4f;
	/** Assigned via the Prefab GUI */
	public Transform groundCheck;
	/** Assigned via the Prefab GUI */
	public PhysicsMaterial2D bouncy;
	/** Assigned via the Prefab GUI */
	public PhysicsMaterial2D slippery;
	/** Time in seconds for the charge jump to be fully charged */
	public float fullChargeJumpTime = 1f;
	public float fullChargeJumpForce = 4000f;
	public float minChargeJumpForce = 2000f;
	/** Number of jumps a player can take without touching the wall or the ground */
	public int maxJumpCount = 2;
	public Sprite[] Charging = new Sprite[3];
	public Sprite[] Jumping = new Sprite[3];
	public Transform SpawnPoint;
	/** The Initial Level the player will start in. See Upgrade() for further info. */
	public int level = 0;
	/** The score required for the player to upgrade to the next level */
	public int levelUpScore = 5;

	private bool grounded = true;
	private Rigidbody2D bodyBox;
	private Transform viewPoint;
	private float x, y, jumpMag, jumpCount = 0;
	private float chargeTime = 0f;
	private int score = 0;
	private SpriteRenderer ren;

	/*
	 * Unity Framework Functions 
	 */
	void Start() {
		score = 0;
		bodyBox = gameObject.GetComponent<Rigidbody2D>();
		viewPoint = gameObject.GetComponent<Transform>();
		ren = gameObject.GetComponent<SpriteRenderer>();
		setChargeSprite();

	}

	void Update() {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		float angle = viewPoint.eulerAngles.z + 90f;
		x = Mathf.Cos(angle * Mathf.Deg2Rad);
		y = Mathf.Sin(angle * Mathf.Deg2Rad);

		if (Input.GetKey(moveLeftKey)) {
			moveLeft();
		}

		if (Input.GetKey(moveRightKey)) {
			moveRight();
		}

		if (grounded) {
			jumpCount = 0;
		}

		if (Input.GetKey(jumpKey)) {
			Debug.Log("Charging JUMP");
			chargeJump();
		}
		if (Input.GetKeyUp(jumpKey) && jumpCount < maxJumpCount) {
			jump();
		}
		Upgrade();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "wall") {
			Debug.Log("DRAW");
			setChargeSprite();
			jumpCount -= 1;
			if (jumpCount < 1) {
				jumpCount = 1;
			}
		}
		if (coll.gameObject.tag == "food") {
			addScore(1);
			Destroy(coll.gameObject);
		}
		if (coll.gameObject.tag == "hazard") {
			score = 0;
			Respawn();
		}
	}


	/*
	 * Accessible functions
	 */
	public void setScore(int newScore) {
		score = newScore;
	}

	/** 
	 * Increments the player score by a given amount
	 * */
	public void addScore(int amount) {
		score += amount;
	}

	public int getScore() {
		return score;
	}

	/**
	 * Upgrade this player to the next avaiable level. New attributes are assigned here.
	 * 
		 * Level = 0 is base level
		 * Level = 1 is next level
		 * Level = 2 is final level
	 * */
	public void Upgrade() {
		if (score != 0 && score % levelUpScore == 0) {
			level++;
		}

		switch (level) {

		case 0:
			gameObject.GetComponent<CircleCollider2D>().sharedMaterial = bouncy;
			setChargeSprite();
			Debug.Log("BOUNCY");
			break;
		case 1: 
			gameObject.GetComponent<CircleCollider2D>().sharedMaterial = bouncy;
			setChargeSprite();
			Debug.Log("SLIP");
			break;
		case 2:
			maxJumpCount = 4;
			setChargeSprite();
			Debug.Log("J");
			break;

		}
	}

	public void Respawn() {
		score = 0;
		level = 0;
		viewPoint.position = SpawnPoint.position;
		viewPoint.eulerAngles = new Vector3(0f, 0f);
		bodyBox.velocity = new Vector2(0f, 0f);
	}

	/*
	 * Internal Movement controls
	 */
	private void moveRight() {
		viewPoint.eulerAngles += (new Vector3(0f, 0f, -turnSpeed));
	}

	private void moveLeft() {
		viewPoint.eulerAngles += (new Vector3(0f, 0f, turnSpeed));
	}

	private void jump() {
		jumpCount++;
		jumpMag = (chargeTime / fullChargeJumpTime *(fullChargeJumpForce - minChargeJumpForce)) + minChargeJumpForce;
		bodyBox.AddForce((new Vector2(x*jumpMag,y*jumpMag)));
		chargeTime = 0f;
		setJumpSprite();

	}

	private void chargeJump() {
		if (chargeTime < fullChargeJumpTime) {
			chargeTime += Time.deltaTime;
		}
	}

	private void setChargeSprite() {
		ren.sprite = Charging[level];
	}

	private void setJumpSprite() {
		StartCoroutine(setJumpAnim());
	}

	private IEnumerator setJumpAnim() {
		Debug.Log("SET JUMP");
		ren.sprite = Jumping[level];
		yield return new WaitForSeconds(1);
		if (jumpCount < maxJumpCount) {
			setChargeSprite();
		}
	}


}
