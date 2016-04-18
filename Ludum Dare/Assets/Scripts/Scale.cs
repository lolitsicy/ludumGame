using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 scale = gameObject.GetComponent<Transform> ().lossyScale;
		MeshRenderer meshRen = gameObject.GetComponent<MeshRenderer> ();
		meshRen.material.mainTextureScale = new Vector2 (scale.x/2, scale.y/2);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
