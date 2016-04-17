using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ToScene : MonoBehaviour {
	/** The name of the scene which will be transitioned too */
	public string SceneName = "TestScene1";

	void OnMouseDown() {
		Debug.Log ("LOADING");
		SceneManager.LoadScene (SceneName);
	}
}
