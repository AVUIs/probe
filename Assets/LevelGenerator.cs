using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

//	public int gridWidth = 10;
//	public int gridHeight = 10;
	public GameObject cell;

	public float cellSize = 5;

	public int variationY = 6;

	//private int[,] grid;
	private GameObject[,] cells;

	/* from processing */
	int gridWidth=127;//31;//has to be odd
	int gridHeight=31;//21;
	float[,] grid;
	int[] keyCells;
	float interval=10;
	int marginY = 2;
	int innerSizeY;

	void drawKeyCells(){
		int increment=2;
		keyCells = new int[gridWidth];
		keyCells[0] = Random.Range(marginY,innerSizeY);
		for(int i=increment;i<gridWidth;i+=increment){
			int randomDistance = Random.Range(-variationY,variationY);
			while(keyCells[i-increment]+randomDistance<marginY || keyCells[i-increment]+randomDistance> innerSizeY-marginY){
				randomDistance = Random.Range(-variationY,variationY);
			}
			keyCells[i]=keyCells[i-increment]+randomDistance;
		}
	}

	void drawTweenCells(int x){
		if(keyCells[x-1]<keyCells[x+1]){
			//if going down
			for(int i=keyCells[x-1]-marginY/2;i<=keyCells[x+1]+marginY/2;i++){
				grid[x,i]=1;
			}
		}else{
			//if going up
			for(int i=keyCells[x-1]+marginY/2;i>=keyCells[x+1]-marginY/2;i--){
				grid[x,i]=1;
			}
		}
	}

	void drawAround(int x, int y){
		if(y>=marginY&&y<innerSizeY){ 
			for(int k=y-marginY;k<=y+marginY;k++){
				grid[x,k]=1;
			}
		}
	}

	// Use this for initialization
	void Start () {

		cells = new GameObject[gridWidth,gridHeight];

		innerSizeY = gridHeight-marginY;
		
		drawKeyCells();
		grid = new float[gridWidth,gridHeight];
		//intialize the grid to 0
		for(int x=0; x<gridWidth;x++){
			for(int y=0; y<gridHeight;y++){
				grid[x,y]=0;
			}
		}
		//draw around and between key cells
		for(int x=0; x<gridWidth;x++){
			if(x%2==0){
				//even: draw around (y axis) key cells
				drawAround(x,keyCells[x]);
			}else if(x<gridWidth-1){
				//odd: draw between key cells
				drawTweenCells(x);
			}
		}

		generateGrid (grid);

	}

	public void generateGrid(float[,] gridToGenerate)	{

		for(int x=0;x<gridWidth;x++)	{
			for(int y=0;y<gridHeight;y++)	{
				Vector3 cellPosition = new Vector3( x, y, 0 );
				cells[x,y] = Instantiate(cell,cellPosition * cellSize,Quaternion.identity) as GameObject;
				cells[x,y].transform.localScale = Vector3.one * cellSize;
				if(gridToGenerate[x,y] == 1)	{
					cells[x,y].GetComponent<MeshRenderer>().enabled = false;
					cells[x,y].GetComponent<BoxCollider2D>().enabled = false;
				}
			}
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
