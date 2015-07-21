using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject objectToFollow;

	public bool gradiatedTracking = true;

	private Vector2 offset = Vector2.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
//		float distance = Vector2.Distance ( new Vector2(Screen.width/2.0f,Screen.height/2.0f)
//		                                   , (Vector2)Camera.main.WorldToScreenPoint(objectToFollow.transform.position));

		Vector2 objectScreenPos = Camera.main.WorldToScreenPoint(objectToFollow.transform.position);

		Vector2 distance = new Vector2( Mathf.Abs (objectScreenPos.x - Screen.width/2.0f), Mathf.Abs(objectScreenPos.y - Screen.height/2.0f) );

		Vector2 threshold;

		if(!gradiatedTracking)
			threshold = new Vector2( Screen.width/4.0f, Screen.height/4.0f );
		else
			threshold = distance;

		if(distance.x >= threshold.x || distance.y >= threshold.y )	{

			transform.position = ( new Vector3(objectToFollow.transform.position.x - offset.x, objectToFollow.transform.position.y - offset.y, transform.position.z) );

		} else {
			
			offset = (Vector2)objectToFollow.transform.position - (Vector2)transform.position;

		}

		//float distance = Vector2.Distance(transform.position, objectToFollow.transform.position);


	}
}
