using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunGen : MonoBehaviour
{
    public enum TileType
    {
        Wall, Floor, Edge, Void, corridorFloor, roomFloor, potion, gold, chest, door, monster
    }

    //Configuration Params
    public int columns = 100;                                 
    public int rows = 100;                                    
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10);
    public IntRange monstersPerRoom;
    public IntRange goldPerRoom;
    public IntRange potionsPerRoom;
    public IntRange chestsPerRoom;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;                    
    public GameObject[] outerWallTiles;                      
    public GameObject player;

    //Map Object Arrays
    private TileType[][] tiles;                           
    //private Room[] rooms;                             
    //private Corridor[] corridors;                      
    private GameObject DungeonMap;                        

    public GameObject TL;
    public GameObject TR;
    public GameObject Vert;
    public GameObject Horz;
    public GameObject BL;
    public GameObject BR;
    public GameObject Single;
    public GameObject Tee;
    public GameObject Void;

    //Entity Arrays
    public GameObject Potion;
    public GameObject Gold;
    public GameObject[] monsters;
    public GameObject Chest;
    public GameObject Door;
    public GameObject Exit;

    void Start()
    {
        DungeonMap = new GameObject("Dungeon Map");

        SetupTilesArray();
        InitalizeWalls();
        CreateRooms();

        //CreateRoomsAndCorridors();

        //etTilesValuesForRooms();
        //SetTilesValuesForCorridors();

        InstantiateTiles();
        //InstantiateOuterWalls();
        
    }

    void Update()
    {
        
    }

    void SetupTilesArray()
    {
        tiles = new TileType[columns][];

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[rows];
        }
    }

    void InitalizeWalls(){
        for (int i = 0; i < tiles.Length; i++){
            for( int j = 0; j < tiles[i].Length; j++){
                tiles[i][j] = TileType.Void;
            }
        }
    }

    void CreateRooms(){
        int rooms = numRooms.Random;
        for(int i = 0; i < rooms; i++){
            int width = roomWidth.Random;
            int height = roomHeight.Random;
            int baseRow = new IntRange(1, rows-height-1).Random;
            int baseCol = new IntRange(1, columns-width-1).Random;

            

            for(int k = baseRow; k < baseRow+height; k++){
                for(int j = baseCol; j < baseCol+width; j++){
                    Debug.Log("Setting "+ k + " " +j);
                    tiles[k][j] = TileType.Floor;
                }
            }

            for(int k = baseRow-1; k < baseRow+height+1;k++){
                SetWall(k, baseCol-1);
                SetWall(k, baseCol+width);
            }
            for(int j = baseCol-1; j < baseCol+width+1; j++){
                SetWall(baseRow-1, j);
                SetWall(baseRow+height, j);
            }
            

        }
    }

    void SetWall(int i, int j){
        if(tiles[i][j] != TileType.Floor){
            tiles[i][j] = TileType.Wall;
        }
    }

    void InstantiateTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if (tiles[i][j] == TileType.Floor){
                    InstantiateFromArray(floorTiles, i, j);
                }

                if (tiles[i][j] == TileType.Wall)
                {
                    Debug.Log("Its a WALL");
                    InstantiateFromArray(wallTiles, i, j);
                }
                if (tiles[i][j] == TileType.Void){
                    InstantiateFromArray(outerWallTiles, i , j);
                }

                // if(spawnTiles[i][j] == TileType.corridorFloor)
                // {
                //     //InstantiateSingle(Gold, i, j);
                // }
                 
                // if (spawnTiles[i][j] == TileType.roomFloor)
                // {
                //     //InstantiateSingle(Potion, i, j);
                // }
                // if (spawnTiles[i][j] == TileType.door && i > 0 && i < tiles.Length-1 && j > 0 && j < tiles[i].Length-1)
                // {
                //     bool bm = (tiles[i][j - 1] == TileType.Edge);
                //     bool tm = (tiles[i][j + 1] == TileType.Edge);
                //     bool ml = (tiles[i - 1][j] == TileType.Edge);
                //     bool mr = (tiles[i + 1][j] == TileType.Edge);
                    
                //     InstantiateSingle(Door, i, j);
                // }
            }
        }

        //Connect Textures
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if (tiles[i][j] == TileType.Edge)
                {
                    //ConnectTextures(i, j);
                }
            }
        }
    }

    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        int randomIndex = Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = DungeonMap.transform;
    }

    void InstantiateSingle(GameObject prefab, float xCoord, float yCoord)
    {
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        GameObject tileInstance = Instantiate(prefab, position, Quaternion.identity) as GameObject;

        tileInstance.transform.parent = DungeonMap.transform;
    } 
}
