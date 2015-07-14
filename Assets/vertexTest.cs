using UnityEngine;
using System.Collections;

public class vertexTest : MonoBehaviour {

	//Mesh mesh;
	//Vector3[] vertices;
	public Vector2[] noiseArray;
	//Random random;
	public int count = 0;

	public Vector3[] originalShape;

	public LineRenderer lineRenderer;

	public Vector2[] mouseBuffer;

	private int mouseBufferReadPointer = 0;

	// Use this for initialization
	void Start () {

		//random = new Random();

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetVertexCount(512);
		lineRenderer.SetColors(Color.red, Color.red);
		lineRenderer.SetWidth(0.01f, 0.01f);

		mouseBuffer = new Vector2[512];
	}
	
	// Update is called once per frame
	void Update () {

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;

		if(count==0)	{

			noiseArray = new Vector2[vertices.Length];
			
			//print (noiseArray.Length);

			originalShape = new Vector3[vertices.Length];

			for(int i=0; i<vertices.Length; i++)	{
				noiseArray[i] = new Vector2(Random.Range(0.0f,1.0f), Random.Range(0.0f,1.0f));
				originalShape[i] = vertices[i];
			}




		}

		count++;

		//print (vertices.Length);

//		for(int i=0; i<vertices.Length; i++)	{
//			vertices[i].x += noiseArray[i] * count * Time.deltaTime;
//		}

		//mesh.vertices = vertices;
//		mesh.RecalculateBounds();

//		Vector2 mousepos = new Vector2(  (Input.mousePosition.x / Screen.width) * 2.0f - 1.0f
//		                               , (Input.mousePosition.y / Screen.height) * 2.0f - 1.0f);

		Vector2 mousepos = new Vector2(  (Input.GetTouch (0).position.x / Screen.width) * 2.0f - 1.0f
		                               , (Input.GetTouch (0).position.y / Screen.height) * 2.0f - 1.0f);

		mouseBuffer[mouseBufferReadPointer] = mousepos;

		int v = 0;
		Vector3 lastVertex;
		while (v < vertices.Length) {
			//vertices[i] += Vector3.up * Time.deltaTime;
			vertices[v].x = originalShape[v].x + noiseArray[v].x * mousepos.x;
			vertices[v].y = originalShape[v].y + noiseArray[v].y * mousepos.y;
			lastVertex = vertices[v];
			//lineRenderer.SetPosition(v%512,vertices[v]);
			v++;
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();

		for(int m=0;m<mouseBuffer.Length;m++)	{
			lineRenderer.SetPosition ((mouseBufferReadPointer+m)%512,mouseBuffer[(mouseBufferReadPointer+m)%512]);
		}

//		mouseBufferReadPointer++;
		if(++mouseBufferReadPointer>=512)
			mouseBufferReadPointer = 0;

	}
}
