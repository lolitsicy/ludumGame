using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	/** The spawning class for this object */
	public GameObject spawnerObject;
	private Spawn spawner;
	void Start() {
		spawner = spawnerObject.GetComponent<Spawn> ();
		if (spawner == null) {
			Debug.Log ("Respawn.cs: Invalid spawnerObject assignment");
		}
	}
	/** Literally the only thing we care about for this object */
	void OnDestroy() {
		spawner.Generate ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "food" || coll.gameObject.tag == "wall") {
			Destroy (gameObject);
		}

	}
}
