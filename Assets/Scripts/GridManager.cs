using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private BuildingBlock _blockPrefab;
    //private TextMesh[,] debugTextArray;
    
    private Dictionary<Vector2, Tile> _tiles;
    private int[,] _costField;
    private int[,] _integrationField;
    private Vector2[,] _flowField;
    private Vector2 _goalNode;

    private float _cellsize;
    private bool _isBuildingPaths;
    private bool _isBuildingBlocks;
    // Start is called before the first frame update
    void Start()
    {
        _cellsize = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        GenerateGrid();
        _costField = new int[_width, _height];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //if (_isBuildingPaths == false)
            //{
            //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    MakeCostField();
            //    MakeIntegrationField(mousePos);
            //    MakeFlowField();

            //    Vector2 id = GetIDFromWorldPos(mousePos);
            //    _tiles.TryGetValue(id, out var t);
            //    t.SetInPassable(false);
            //    t.SetIsRough(false);
            //}
            //else
            //{
            //    Debug.Log("Happens");
            //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    Vector2 id = GetIDFromWorldPos(mousePos);
            //    _costField[(int)id.x, (int)id.y] = byte.MaxValue;
            //    _tiles.TryGetValue(id, out var t);
            //    t.SetInPassable(true);
            //    t.SetIsRough(false);
            //}
            if (_isBuildingPaths)
            {
                Debug.Log("Path");
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 id = GetIDFromWorldPos(mousePos);
                _costField[(int)id.x, (int)id.y] = byte.MaxValue;
                _tiles.TryGetValue(id, out var t);
                t.SetInPassable(true);
                t.SetIsRough(false);
            }
            else if (_isBuildingBlocks) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                BuildBlock(mousePos);
                Vector2 id = GetIDFromWorldPos(mousePos);
                _costField[(int)id.x, (int)id.y] = byte.MaxValue;
            }
            else 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                MakeCostField();
                MakeIntegrationField(mousePos);
                MakeFlowField();

                Vector2 id = GetIDFromWorldPos(mousePos);
                _tiles.TryGetValue(id, out var t);
                t.SetInPassable(false);
                t.SetIsRough(false);
            }


        }
        
        if (Input.GetMouseButtonDown(1))
        {

            Debug.Log("Erase");
            //SetVelocityAtPos(mousePos, new Vector2(-1f, -1f));
            if (_isBuildingPaths == false)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 id = GetIDFromWorldPos(mousePos);
                _costField[(int)id.x, (int)id.y] = 1;
                _tiles.TryGetValue(id, out var t);
                t.SetInPassable(false);
                t.SetIsRough(false);
                t._isGoal = false;
            }
            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 id = GetIDFromWorldPos(mousePos);
                _costField[(int)id.x, (int)id.y] = 4;
                _tiles.TryGetValue(id, out var t);
                //t.SetInPassable(true);
                t.SetIsRough(true);
                //t.SetInPassable(false);
            }
        }
      
        if (Input.GetKeyUp(KeyCode.Alpha1)) 
        {
            ShowCostfield();
            //ShowText();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
           ShowIntegrationfield();
           //ShowText();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ShowFlowfield();
            //ShowText();
            //ShowSprites();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            if (_isBuildingPaths == false) 
            {
                _isBuildingPaths = true;
                _isBuildingBlocks = false;
            }
            else 
            {
                _isBuildingPaths = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            //var cube= GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(_width/2, _height/2, -5f);
            //cube.transform.localScale = new Vector3(_cellsize, _cellsize, 5);
            if (_isBuildingBlocks == false)
            {
                _isBuildingBlocks = true;
                _isBuildingPaths = false;
            }
            else
            {
                _isBuildingBlocks = false;
            }
        }

        //Debug.Log(_flowField[(int)_goalNode.x,(int)_goalNode.y].ToString());
    }

    public bool GetIsBuildingBlocks() 
    {
        return _isBuildingBlocks;
    }

    public bool GetIsBuildingPaths()
    {
        return _isBuildingPaths;
    }
    void GenerateGrid() 
    {
        _tiles = new Dictionary<Vector2, Tile>();
        _flowField = new Vector2[_width, _height];
        for (int x = 0; x < _width; x++) 
        {
            for(int y = 0; y < _height; y++) 
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}{y}";

                //CHECKBOARD PATERN
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);

                //spawnedTile.ColorIn(isOffset); GONNA CALL COLOR IN TILE ITSELF AND JUST SET OFFSET HERE
                spawnedTile._isOffset = isOffset;

                _tiles[new Vector2(x, y)] = spawnedTile;

                //FLOWFIELD VELOCITY TEST
                
               // _flowField[x, y] = new Vector2(0,0);
            }
        }

        //CENTER CAMERA TO MIDDLE GRID
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f,-10);

       
    }



    public Tile GetTileFromPos(Vector2 Pos) 
    {       
        Vector2 loc = new Vector2(Mathf.Ceil(Pos.x + _cellsize / 2), Mathf.Ceil(Pos.y + _cellsize / 2));
        //BECAUSE ITS ZERO BASED?
        loc.x--;
        loc.y--;
        if (_tiles.TryGetValue(loc, out var _tile))
        {
            return _tile;
        }
        return null;
    }

    public Vector2 GetVelocityFromPos(Vector2 pos) 
    {
        Vector2 velocity;
        Vector2 ID = GetIDFromWorldPos(pos);
        velocity =_flowField[(int)ID.x,(int) ID.y];
        return velocity;
    }

    public void SetVelocityAtPos(Vector2 pos,Vector2 newVelocity)
    {
        Vector2 ID = GetIDFromWorldPos(pos);
        _flowField[(int)ID.x, (int)ID.y] = newVelocity;
    }

    private Vector2 GetIDFromWorldPos(Vector2 worldPos) 
    {
        Vector2 ID = new Vector2(Mathf.Ceil(worldPos.x + _cellsize / 2), Mathf.Ceil(worldPos.y + _cellsize / 2));
        //BECAUSE ITS ZERO BASED?
        ID.x--;
        ID.y--;
        return ID;
    }

    private void ShowFlowfield() 
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
           
                _tiles.TryGetValue(new Vector2(x, y),out var t);
                //t.SetString(_flowField[x,y].ToString());
                t.ShowSprites(true);

            }
        }
    }

    private void ShowCostfield()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

                _tiles.TryGetValue(new Vector2(x, y), out var t);
                t.SetString(_costField[x, y].ToString());
                t.ShowSprites(false);
            }
        }
    }

    private void ShowIntegrationfield()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

                _tiles.TryGetValue(new Vector2(x, y), out var t);
                t.SetString(_integrationField[x, y].ToString());
                t.ShowSprites(false);
            }
        }
    }

    private void MakeCostField()
    {
        //_costField = new int[_width, _height]; //PLACE IT SOMEWHERE ELSE ELSE IT WILL ALWAYS RESET EVEN IF ABOVE 1
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_costField[x, y] < 2) 
                {

                    _costField[x, y] = 1; //ONLY RESET OR MAKE NEW IF HAVENT PLACED INPASSABLE OR ROUGH TERAIN

                    //RESET GOAL NODE COLOR
                    _tiles.TryGetValue(new Vector2(x, y), out var t);
                    t._isGoal = false;
                }
            }
        }
    }

    private void MakeIntegrationField(Vector2 goalPos) 
    {
        
        _integrationField = new int[_width, _height];
        //LIST TRASH I GUESS? TRY QUEUE
        //List<Vector2> openlist;
        Queue<Vector2> openlist = new Queue<Vector2>();
        //RESET ALL VALUES TO SOMETHING BIG
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _integrationField[x, y] = int.MaxValue;
            }
        }

        //SET GOAL NODE TO 0 AND ADD TO QUEUE
        //Vector2 goalnode = new Vector2(_width / 2, _height / 2);//DO MIDDLE FOR TEST
        Vector2 goalnode = GetIDFromWorldPos(goalPos); //TEST
        _goalNode = goalnode;
        _costField[(int)goalnode.x, (int)goalnode.y] = 0;
        _integrationField[(int)goalnode.x,(int)goalnode.y] = 0;
        _flowField[(int)goalnode.x, (int)goalnode.y] = new Vector2(0, 0);
        openlist.Enqueue(goalnode);

        //COLOR GOAL NODE
        _tiles.TryGetValue(goalnode, out var t);
        t._isGoal = true;
        while (openlist.Count > 0) 
        {
            Vector2 curID = openlist.Dequeue();
            List<Vector2> curNeighbours = GetNeighboursID(curID);
            foreach (Vector2 curNeighbour in curNeighbours) 
            {
                if (GetCostFieldCostAtID(curNeighbour) == byte.MaxValue) 
                {
                    continue;
                }


                if (GetCostFieldCostAtID(curNeighbour) + GetIntegrationCostAtID(curID) < GetIntegrationCostAtID(curNeighbour))  
                {
                    _integrationField[(int)curNeighbour.x, (int)curNeighbour.y] = _costField[(int)curNeighbour.x,(int)curNeighbour.y] + _integrationField[(int)curID.x, (int)curID.y];
                    openlist.Enqueue(curNeighbour);
                }
            }
        }
    }

    private void MakeFlowField() 
    {
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 curID = new Vector2(x, y);
                List<Vector2> neighbours = GetNeighboursAllDirectionsID(curID);
                int integrationCost = GetIntegrationCostAtID(curID);

                //if (integrationCost != int.MaxValue) 
                //{

                 foreach (Vector2 curNeighbour in neighbours) 
                 {
                    
                     if (GetIntegrationCostAtID(curNeighbour) < integrationCost) 
                     {
                        integrationCost = GetIntegrationCostAtID(curNeighbour);
                       
                        _flowField[(int)curID.x, (int)curID.y] = curNeighbour - curID; //THIS SHOULD WORK AS DIRECTION


                            //FOR SPRITES
                        _tiles.TryGetValue(curID, out var t);
                        t._direction = _flowField[(int)curID.x, (int)curID.y];

                       
                     }
                 }
           
            }
        }

        //WORKS WITHOUT BUT FOR PURE VISUAL TO GO THROUGH CAUSE IT WELL NEVER GET INTO THE IFF
        _tiles.TryGetValue(_goalNode, out var tile);
        tile._direction = Vector2.zero;

    }
    private List<Vector2> GetNeighboursID(Vector2 ID) 
    {
        List<Vector2> neighbours = new List<Vector2>();
        Vector2 top = new Vector2(ID.x, ID.y +1);
        Vector2 right = new Vector2(ID.x + 1 , ID.y);
        Vector2 bottom = new Vector2(ID.x, ID.y - 1);
        Vector2 left = new Vector2(ID.x -1 , ID.y);

        if (top.y < _height) 
        {

            neighbours.Add(top);
        }
        if (right.x < _width)
        {

            neighbours.Add(right);
        }
        if (bottom.y >= 0)
        {

            neighbours.Add(bottom);
        }
        if (left.x >= 0)
        {

            neighbours.Add(left);
        }
        
        
     

        return neighbours;
    }


    private List<Vector2> GetNeighboursAllDirectionsID(Vector2 ID)
    {
        List<Vector2> neighbours = new List<Vector2>();
        Vector2 north = new Vector2(ID.x, ID.y + 1);
        Vector2 east = new Vector2(ID.x + 1, ID.y);
        Vector2 south = new Vector2(ID.x, ID.y - 1);
        Vector2 west = new Vector2(ID.x - 1, ID.y);

        Vector2 northEast = new Vector2(ID.x + 1, ID.y + 1);
        Vector2 southEast = new Vector2(ID.x + 1, ID.y - 1);
        Vector2 northWest = new Vector2(ID.x - 1, ID.y + 1);
        Vector2 southWest = new Vector2(ID.x - 1, ID.y - 1);


        if (north.y < _height)
        {

            neighbours.Add(north);
        }
        if (east.x < _width)
        {

            neighbours.Add(east);
        }
        if (south.y >= 0)
        {

            neighbours.Add(south);
        }
        if (west.x >= 0)
        {

            neighbours.Add(west);
        }

        if (northEast.y < _height && northEast.x < _width) 
        {
            neighbours.Add(northEast);
        }

        if (southEast.y >= 0 && southEast.x < _width)
        {
            neighbours.Add(southEast);
        }

        if (northWest.y < _height && northWest.x >= 0)
        {
            neighbours.Add(northWest);
        }

        if (southWest.y >= 0 && southWest.x >= 0)
        {
            neighbours.Add(southWest);
        }



        return neighbours;
    }

    private int GetCostFieldCostAtID(Vector2 ID) 
    {
        return _costField[(int)ID.x, (int)ID.y];
    }

    private int GetIntegrationCostAtID(Vector2 ID)
    {
        return _integrationField[(int)ID.x, (int)ID.y];
    }


    private void BuildBlock(Vector2 mousePos)
    {
        //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(_width / 2, _height / 2, -5f);
        //cube.transform.localScale = new Vector3(_cellsize, _cellsize, 5);
        Vector2 id = GetIDFromWorldPos(mousePos);
        _tiles.TryGetValue(id, out var t);
        Vector2 startPos = t.transform.localPosition;
        //cube.transform.position = new Vector3(startPos.x,startPos.y,-5f);
        Vector3 pos = new Vector3(startPos.x, startPos.y, -5f);
        //cube.transform.localScale = new Vector3(_cellsize, _cellsize, 5);
        Vector3 scale = new Vector3(_cellsize, _cellsize, 5);
        BuildingBlock b = Instantiate(_blockPrefab, pos,Quaternion.identity);
        b.transform.localScale = scale;
        b._manager = this;

    }
}



