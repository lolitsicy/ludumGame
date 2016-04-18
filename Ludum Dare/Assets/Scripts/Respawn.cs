using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	/** The spawning class for this object */
	public GameObject spawnerObject;
	private Spawn spawner;
	void Start() {
		spawner = Camera.main.GetComponent<Spawn> ();
		if (spawner == null) {
			Debug.Log ("Main Camera does not hold the Spawn.cs script");
		}
	}
	/** Literally the only thing we care about for this object */
	void OnDestroy() {
		spawner = Camera.main.GetComponent<Spawn> ();
		if (spawner == null) {
			Debug.Log ("Main Camera does not hold the Spawn.cs script");
		}
		spawner.Generate ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "food" || coll.gameObject.tag == "wall") {
			Destroy (gameObject);
		}

	}
}
