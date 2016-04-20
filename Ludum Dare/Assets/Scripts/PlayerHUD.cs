using UnityEngine;
using System.Collections;

/**
 *  Container class for all Player visible stats.
 * */
public class PlayerHUD : MonoBehaviour {
	/** The player this HUD will monitor */
	public GameObject Player;

	private PlayerControler playerControl;

	private int scoreCurrent, scoreMax;
	private TextMesh scoreMesh;
	private string baseScoreString;

	// Use this for initialization
	void Start () {
		scoreMesh = gameObject.GetComponent<TextMesh>();
		baseScoreString = scoreMesh.text;
		playerControl = Player.GetComponent<PlayerControler>();
		scoreMax = playerControl.levelUpScore;
	}

	public void setScore(int score) {
		scoreCurrent = score;
		scoreMesh.text = baseScoreString + " " + scoreCurrent + "/" + scoreMax;
		//Insert score bar code here
	}

	public int getScore() {
		return scoreCurrent;
	}
}
