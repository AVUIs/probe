using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour {

	//public float exp = 1.0f;

	public bool USE_MOUSE = false;

	public GameObject sphere;

	private Vector2 targetPos = new Vector2 (3.0f , 4.0f );

	private Rigidbody2D rb;

	private Camera cam;

	private Vector2 previousTouchPosition;

	private Vector2 transformPositionOnRelease;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody2D>();
		rb.drag = 6.5f;

		previousTouchPosition = new Vector2( Screen.width/2, Screen.height/2 );
		transformPositionOnRelease = Vector2.zero;

		cam = Camera.main;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if( USE_MOUSE ) {
			//if(Input.GetMouseButton)
				targetPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );// / Screen.height) * 2.0f - 1.0f);
		} else {
			if(Input.touchCount>0) {
				targetPos = new Vector2( Input.GetTouch(0).position.x, Input.GetTouch (0).position.y );
				previousTouchPosition = targetPos;
				transformPositionOnRelease = transform.position;
				targetPos = cam.ScreenToWorldPoint(targetPos);	
				sphere.GetComponent<MeshRenderer>().enabled = true;
			} else {
				targetPos = transformPositionOnRelease;
				sphere.GetComponent<MeshRenderer>().enabled = false;
				//targetPos = previousTouchPosition;
			}
		}

		//cam.transform.Translate ( new Vector3(transform.position.x - cam.transform.position.x, transform.position.y - cam.transform.position.y, 0.0f) );


		sphere.transform.position = targetPos;

		float distanceToTarget = Vector2.Distance(transform.position, targetPos);

		Vector2 dist = (Vector2)targetPos-(Vector2)transform.position;

		//float tempScale = 0.1f;

		float forceScale = 0.01f * distanceToTarget / Time.deltaTime;

		//rb.GetPointVelocity

		Vector2 forceVector = forceScale * (targetPos - (Vector2)transform.position).normalized;
		//Vector2 forceVector = dist * dist;

		rb.AddForce ( forceVector, ForceMode2D.Impulse );

//		transform.position.x += (dist.x * 0.1f);
//		transform.position.y += (dist.y * 0.1f);



	}
}
