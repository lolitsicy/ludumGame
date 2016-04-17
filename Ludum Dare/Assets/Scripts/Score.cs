using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public PlayerControler playerTracking;
	private TextMesh scoreText;
	private string baseText;

	// Use this for initialization
	void Start () {
		scoreText = gameObject.GetComponent<TextMesh> ();
		baseText = scoreText.text;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = baseText + playerTracking.getScore ();
	}
}
