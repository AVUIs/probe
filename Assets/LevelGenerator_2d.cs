using UnityEngine;
using System.Collections;

public class LevelGenerator_2d : MonoBehaviour {

	public GameObject grid;

	public int numGrids = 2;

	private GameObject[] grids;
	private GridGenerator_2d[] gridGenerators;

	// Use this for initialization
	void Start () {

		grids = new GameObject[numGrids];
		gridGenerators = new GridGenerator_2d[numGrids];

		grids[0] = Instantiate (grid,new Vector3(0,0,0),Quaternion.identity) as GameObject;
		gridGenerators[0] = grids[0].GetComponent<GridGenerator_2d>();
		gridGenerators[0].generateGrid (new Vector2(0,0));
		Vector2 newStartPos = new Vector2(gridGenerators[0].endPos.x, gridGenerators[0].endPos.y);
		for(int i=1;i<numGrids;i++)	{
			Vector2 offset = new Vector2(gridGenerators[i-1].sideToEndOn ? 0 : gridGenerators[i-1].gridWidth, gridGenerators[i-1].sideToEndOn ? gridGenerators[i-1].gridHeight : 0);
			//offset.x =
			//grids[i] = Instantiate (grid,new Vector3(newStartPos.x + offset.x,newStartPos.y + offset.y,0),Quaternion.identity) as GameObject;

			print (gridGenerators[i-1].getAbsoluteEndPosition());

			grids[i] = Instantiate (grid,gridGenerators[i-1].getAbsoluteEndPosition(),Quaternion.identity) as GameObject;
			gridGenerators[i] = grids[i].GetComponent<GridGenerator_2d>();
			gridGenerators[i].generateGrid (newStartPos);
			newStartPos = new Vector2(gridGenerators[i].endPos.x, gridGenerators[i].endPos.y);
			//print (newStartPos);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
