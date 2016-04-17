using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	/** Object which will be spawned. Preferably a prefab */
	public GameObject objectToSpawn;
	/** How much space should surround this object when its spawened */
	public float clearanceRadius = 10f;

	void Start()
	{
		//Initial Spawn
		Invoke("Generate", 0f);
		//Uncomment to Test bounds of spawn space
//		InvokeRepeating ("Generate", 0f, 0.5f);
	}

	public void Generate()
	{
		float x = Mathf.Round(Random.Range(10, Camera.main.pixelWidth - 10));
		float y = Mathf.Round(Random.Range(10, Camera.main.pixelHeight - 10));

		Vector3 Target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
		Target.x = Mathf.Round (Target.x);
		Target.y = Mathf.Round (Target.y);
		Target.z = 0f;
		if (Physics.CheckSphere (Target, clearanceRadius)) {
			Generate ();
		} else {
			if (Physics.CheckSphere (Target, clearanceRadius)) {
				Generate ();
			}
			Instantiate(objectToSpawn, Target, Quaternion.identity);
		}
	}
}