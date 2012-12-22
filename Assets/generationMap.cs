using UnityEngine;
using System.Collections;

public class generationMap : MonoBehaviour {
	
	//Hexagone object
	public GameObject HexWater;
	public GameObject HexPlaine;
	public GameObject HexBeach;
	
	public int gridWidth;
	public int gridHeight;
	
	public float ratioWater;
	public int waterHexNumber;
	
	private float hexWidth;
    private float hexHeight;
	
	private GameObject[,] mapTable;
	
	// Use this for initialization
	void Start () {
		mapTable = new GameObject[gridWidth,gridHeight];
		generateMap();
		checkMap();
		setSizes();
        createGrid();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void setSizes()
    {
        //renderer component attached to the Hex prefab is used to get the current width and height
        hexWidth = HexPlaine.renderer.bounds.size.x;
        hexHeight = HexPlaine.renderer.bounds.size.y;
    }
	
	Vector3 calcInitPos()
    {
        Vector3 initPos;
        //the initial position will be in the left upper corner
        initPos = new Vector3(-hexWidth * gridWidth / 2f + hexWidth / 2, gridHeight / 2f * hexHeight - hexHeight / 2, 0);

        return initPos;
    }
	
	//method used to convert hex grid coordinates to game world coordinates
    public Vector3 calcWorldCoord(Vector2 gridPos)
    {
        //Position of the first hex tile
        Vector3 initPos = calcInitPos();
        //Every second row is offset by half of the tile width
        float offset = 0;

        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x =  initPos.x + offset + gridPos.x * hexWidth;
        //Every new line is offset in z direction by 3/4 of the hexagon height
        float y = initPos.y - gridPos.y * hexHeight * 0.75f;
        return new Vector3(x, y, 0);
    }
	
	//Finally the method which initialises and positions all the tiles
    void createGrid()
    {
        //Game object which is the parent of all the hex tiles
        GameObject hexGridGO = new GameObject("HexGrid");
 
        for (float y = 0; y < gridHeight; y++)
        {
            for (float x = 0; x < gridWidth; x++)
            {
                //GameObject assigned to Hex public variable is cloned
                GameObject hexPlaine = mapTable[(int)x,(int)y];
                //Current position in grid
                Vector2 gridPos = new Vector2(x, y);
                hexPlaine.transform.position = calcWorldCoord(gridPos);
                hexPlaine.transform.parent = hexGridGO.transform;
            }
        }
    }
	
	void generateMap(){
		for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
				if(x<waterHexNumber || y<waterHexNumber || x >= gridWidth-waterHexNumber || y >= gridHeight-waterHexNumber)
					mapTable[x,y] = (GameObject)Instantiate(HexWater);
				else{
					if(y%2==0){
						if(mapTable[x-1,y].name.Equals("HexWater(Clone)") || mapTable[x,y-1].name.Equals("HexWater(Clone)") ||
							mapTable[x-1,y-1].name.Equals("HexWater(Clone)") || x == gridWidth-(waterHexNumber+1) || y == gridHeight-(waterHexNumber+1)){
							generateBeach(x,y);
						}else{
							mapTable[x,y] = (GameObject)Instantiate(HexPlaine);
						}
					}else{
						if(mapTable[x-1,y].name.Equals("HexWater(Clone)") || mapTable[x,y-1].name.Equals("HexWater(Clone)") ||
							mapTable[x+1,y-1].name.Equals("HexWater(Clone)") || x == gridWidth-(waterHexNumber+1) || y == gridHeight-(waterHexNumber+1)){
							generateBeach(x,y);
						}else{
							mapTable[x,y] = (GameObject)Instantiate(HexPlaine);
						}
					}
					
	                /*if(x%2==0)
						mapTable[x,y] = (GameObject)Instantiate(HexPlaine);
					else
						mapTable[x,y] = (GameObject)Instantiate(HexBeach);*/
				}
            }
        }	
	}
	
	void generateBeach(int x, int y){
		float r = Random.value;
		if(r<ratioWater)
			mapTable[x,y] = (GameObject)Instantiate(HexWater);
		else
			mapTable[x,y] = (GameObject)Instantiate(HexBeach);
	}
	
	void checkMap(){
		for (int y = 1; y < gridHeight-waterHexNumber; y++)
        {
            for (int x = 1; x < gridWidth-waterHexNumber; x++)
            {
				if(mapTable[x,y].name.Equals("HexPlaine(Clone)")){
					if(y%2==0){
						if(mapTable[x-1,y+1].name.Equals("HexWater(Clone)") || mapTable[x,y+1].name.Equals("HexWater(Clone)") || mapTable[x+1,y].name.Equals("HexWater(Clone)")){
							Destroy(mapTable[x,y]);
							mapTable[x,y] = (GameObject)Instantiate(HexBeach);
						}
					}
					else{
						if(mapTable[x+1,y+1].name.Equals("HexWater(Clone)") || mapTable[x,y+1].name.Equals("HexWater(Clone)") || mapTable[x+1,y].name.Equals("HexWater(Clone)")){
							Destroy(mapTable[x,y]);
							mapTable[x,y] = (GameObject)Instantiate(HexBeach);
						}
					}
				}
            }
        }	
	}
}
