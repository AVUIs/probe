using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridUnitController_2d : MonoBehaviour, IComparable<GridUnitController_2d> {

//	public bool isStart
//	{ 	get { return isStart; }
//		set { isStart = value; } }
//	public bool isEnd
//	{ 	get { return isEnd; }
//		set { isEnd = value; } }

	public bool isStart = false;
	public bool isEnd = false;
	public bool isPlaced = false;
	public bool isVisible = true;

	public Material material_black;
	public Material material_white;

	private Vector2 size = new Vector2(1,1);

	public List<GridUnitController_2d> neighbours = new List<GridUnitController_2d>();

	//	private List neighbours;

	// Use this for initialization
	void Start () {
	
		//GetComponent<MeshRenderer>().enabled = false;
		if(isStart || isEnd)	{
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3(transform.position.x,transform.position.y, 0);
		}

	}
	
	// Update is called once per frame
	void Update () {
	

		
	}

	public void setSize(float x, float y)	{
		size.x = x;
		size.y = y;
	}

	public void addNeighbour(GridUnitController_2d neighbour)	{

		neighbours.Add ( neighbour );

	}

	public int CompareTo(GridUnitController_2d other)	{

		return 0;

	}

	public void setVisible(bool value)	{
		isVisible = value;
		//GetComponent<MeshRenderer>().enabled = isVisible;
		GetComponent<MeshRenderer>().enabled = true;
		if(value)
			GetComponent<MeshRenderer>().material = material_black;
		else
			GetComponent<MeshRenderer>().material = material_white;
	}

	public void setStart()	{
		isStart = true;
		setVisible (false);
		isPlaced = true;
	}

	public void setEnd()	{
		isEnd = true;
		setVisible (false);
		isPlaced = true;
	}

}
