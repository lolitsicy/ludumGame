using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
	/** Object which will be spawned. Preferably a prefab */
	public GameObject objectToSpawn;
	/** Only add objects of the Point Prefab */
	public Transform[] SpawnPoints;

	void Start()
	{
		//Initial Spawn
		Invoke("Generate", 0f);
		//Uncomment to Test bounds of spawn space
//		InvokeRepeating ("Generate", 0f, 0.5f);
	}

	public void Generate()
	{
		int i = Mathf.RoundToInt(Random.Range(0, SpawnPoints.Length - 1));
		Instantiate (objectToSpawn, SpawnPoints[i].position, Quaternion.identity);
	}
}