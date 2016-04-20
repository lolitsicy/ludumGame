using UnityEngine;
 using System.Collections;
 
 public class SmoothCamera2D : MonoBehaviour {
     
	public float dampTime = 0.05f;
	private Vector3 velocity = Vector3.zero;
	private float velocityf = 0f;
	public Transform player1, player2;
	public float minSizeY = 5f;
	private Camera cam;
	 // Update is called once per frame

	void Start () {
		cam = GetComponent<Camera>();
	}
	void Update () 
	{
//		if (target1 && target2) {
//		     Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
//		     Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//		     Vector3 destination = transform.position + delta;
//		     transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
//		 }
		if (player1 && player2) {
			transform.position = Vector3.SmoothDamp (transform.position, CalcMid(player1.position, player2.position), ref velocity, dampTime);
			cam.orthographicSize = Mathf.SmoothDamp (cam.orthographicSize, ComputeLargestSize(player1.position, player2.position) , ref velocityf, dampTime);
		}

	}

	Vector3 CalcMid(Vector3 pos1, Vector3 pos2) {
		Vector3 middle = (pos1 + pos2) / 3.0f;
		return new Vector3(
			middle.x,
			middle.y,
			cam.transform.position.z
		);
	}

	Vector3 CalcPos(Vector3 pos1, Vector3 pos2) {
		Vector3 middle = (pos1 + pos2) * 0.5f;
	     return new Vector3(
	         middle.x,
	         middle.y,
			cam.transform.position.z
	     );
	 }

	float CalcSize(Vector3 pos1, Vector3 pos2) {
	     //horizontal size is based on actual screen ratio
	     float minSizeX = minSizeY * Screen.width / Screen.height;

	     //multiplying by 0.5, because the ortographicSize is actually half the height
		float width = Mathf.Abs(pos1.x - pos2.x) * 0.5f;
		float height = Mathf.Abs(pos1.y - pos2.y) * 0.5f;

	     //computing the size
	     float camSizeX = Mathf.Max(width, minSizeX) + 10f;
	     return Mathf.Max(height,
	         camSizeX * Screen.height / Screen.width, minSizeY);
	 }

	float ComputeLargestSize(Vector3 pos1, Vector3 pos2) {
		Vector3 centre = new Vector3 (0f, 0f);
		float size1, size2, size3;
		size1 = CalcSize (pos1, pos2);
		size2 = CalcSize (pos1, centre);
		size3 = CalcSize (pos2, centre);
		if (size1 >= size2 && size1 >= size3)
			return size1;
		else if (size2 >= size1 && size2 >= size3)
			return size2;
		else
			return size3;
	}
 }
