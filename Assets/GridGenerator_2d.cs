using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using System;

public class GridGenerator_2d : MonoBehaviour {

	// publics
	public GameObject gridUnit;

	[HideInInspector]
	public Vector2 startPos;
	[HideInInspector]
	public Vector2 endPos;
	[HideInInspector]
	public bool sideToEndOn;
	[HideInInspector]
	public int gridWidth = 10;
	[HideInInspector]
	public int gridHeight = 10;

	// privates :-o
	private GameObject[,] grid;// = new GameObject[gridWidth][gridHeight];
	private GridUnitController_2d[,] gridUnitControllers;
	private GridUnitController_2d gridUnit_start;
	private GridUnitController_2d gridUnit_end;

	//public List<GridGenerator> gridNeighbours;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



	}

	public void generateGrid (Vector2 startPosVector)	{

		sideToEndOn = (Random.Range (0,1) == 1) ? true : false;

		if(!sideToEndOn)	{					// right
			endPos.x = gridWidth-1;
			endPos.y = Random.Range (0,gridHeight);
		} else {									// top
			endPos.x = Random.Range (0,gridWidth);
			endPos.y = gridHeight-1;
		}

//		startPos = startPosVector;
//
//		bool done = false;
//		while(!done)	{
//			int sideToEndOn = Random.Range (0,3);
//			if(sideToEndOn == 0)	{
//				endPos.x = 0;
//				endPos.y = Random.Range (0,gridHeight-1);
//				if(!(startPos.x==0))
//					done = true;
//			} else if(sideToEndOn == 1)	{
//				endPos.x = gridWidth-1;
//				endPos.y = Random.Range (0,gridHeight-1);
//				if(!(startPos.x==gridWidth-1))
//					done = true;
//			} else if(sideToEndOn == 2)	{
//				endPos.x = Random.Range (0,gridWidth-1);
//				endPos.y = 0;
//				if(!(startPos.y==0))
//					done = true;
//			} else if(sideToEndOn == 3)	{
//				endPos.x = Random.Range (0,gridWidth-1);
//				endPos.y = gridHeight-1;
//				if(!(startPos.y==gridHeight-1))
//					done = true;
//			}
//		}
//
//		if(startPos.x == 0)	{
//			endPos.x = gridWidth-1;
//		}
//		if(startPos.y == 0)	{
//			endPos.y = gridHeight-1;
//		}
//		if(startPos.x == gridWidth-1)	{
//			endPos.x = 0;
//		}
//		if(startPos.y == gridHeight-1)	{
//			endPos.y = 0;
//		}


		grid = new GameObject[gridWidth,gridHeight];
		gridUnitControllers = new GridUnitController_2d[gridWidth,gridHeight];
		
		for(int x=0; x<gridWidth; x++)	{
			for(int y=0; y<gridHeight; y++)	{
				float xpos = (x-startPos.x) * 1.0f;//(x/(float)gridWidth)*gridWidth-(gridWidth*0.5f) * 1.0f;
				float ypos = (y-startPos.y) * 1.0f;//(y/(float)gridHeight)*gridHeight-(gridHeight*0.5f) * 1.0f;
				
				grid[x,y] = Instantiate(gridUnit, new Vector3(xpos+transform.position.x, ypos+transform.position.y, 0),Quaternion.identity) as GameObject;
				grid[x,y].transform.SetParent(this.transform);
				gridUnitControllers[x,y] = grid[x,y].GetComponent<GridUnitController_2d>();
				
				if(x==startPos.x && y==startPos.y)	{
					gridUnitControllers[x,y].setStart();
					gridUnit_start = gridUnitControllers[x,y];
				}
				if(x==endPos.x && y==endPos.y)	{
					gridUnitControllers[x,y].setEnd();
					gridUnit_end = gridUnitControllers[x,y];
				}
				
			}
		}
		
		for(int x=0; x<gridWidth; x++)	{
			for(int y=0; y<gridHeight; y++)	{
				// set neighbours
				if(x < gridWidth-1)
					gridUnitControllers[x,y].addNeighbour( gridUnitControllers[x+1,y] );
				if(x > 0)
					gridUnitControllers[x,y].addNeighbour( gridUnitControllers[x-1,y] );
				if(y < gridHeight-1)
					gridUnitControllers[x,y].addNeighbour( gridUnitControllers[x,y+1] );
				if(y > 0)
					gridUnitControllers[x,y].addNeighbour( gridUnitControllers[x,y-1] );
			}
		}

		bool completed = false;

		GridUnitController_2d currentGridUnit = gridUnit_start;

		while(!completed)	{

			int numNeighbours = currentGridUnit.neighbours.Count;
			int neighbourToPlace = Random.Range(0,numNeighbours);

//			while(gridUnit.neighbours[neighbourToPlace] !=

			currentGridUnit.neighbours[neighbourToPlace].setVisible(false);
			currentGridUnit.neighbours[neighbourToPlace].isPlaced = true;
			currentGridUnit = currentGridUnit.neighbours[neighbourToPlace];
			if(currentGridUnit == gridUnit_end)
				completed = true;

		}

//		for(int x=0; x<gridWidth; x++)	{
//			for(int y=0; y<gridHeight; y++)	{
//				GridUnitController gridUnit = gridUnitControllers[x,y];
//				int numNeighbours = gridUnit.neighbours.Count;
//				int neighbourToPlace = Random.Range(0,numNeighbours);
//				int numInvisibleNeighbours = 0;
//				for(int i=0;i<numNeighbours;i++)	{
////					if(!gridUnit.isVisible)
////						break;
//					GridUnitController neighbour = gridUnitControllers[x,y].neighbours[i];
//					if(!neighbour.isVisible)
//						numInvisibleNeighbours++;
//					if(numInvisibleNeighbours >= 2)
//						break;
//					if(i==neighbourToPlace)	{
//						gridUnitControllers[x,y].setVisible(false);
//						gridUnitControllers[x,y].isPlaced = true;
//						break;
//					}
//				}
//
//			}
//		}
//
//		for(int x=0; x<gridWidth; x++)	{
//			for(int y=0; y<gridHeight; y++)	{
//				if(!gridUnitControllers[x,y].isPlaced)	{
//					gridUnitControllers[x,y].setVisible (true);
//					//print ("placed visible");
//					gridUnitControllers[x,y].isPlaced = true;
//				}
//			}
//		}

	}

	public Vector3 getAbsoluteEndPosition()	{
		return gridUnit_end.transform.position;
	}


}





















