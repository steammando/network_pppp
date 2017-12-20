using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkMapCreate : MonoBehaviour {

    public GameObject tile;
    public GameObject UnderGround;
    public GameObject Wall;

    private int tile_row = 5;
    private int tile_column = 20;

    public struct WallStruct
    {
        public GameObject obj;
        public Transform tf;//For position conversion
        public bool active; //Purpose of activation
        public Vector3 pos; //For storing location information
    }
    public WallStruct[] Walls;

    public struct TileSturct
    {
        public GameObject obj;
        public Transform tf;//For position conversion
        public bool active; //Purpose of activation
        public Vector3 pos; //For storing location information
    }
    public TileSturct[,] tiles; //GameObject to use on the floor (Prefab) Landscape * Portrait
    private float tileGap = 0.65f; //Adjust the spacing between tiles


    //The variables and methods for adding bombs are the same as TileStruct.
    public GameObject obstacle;
    public int obsNum;
    private int r, l; //Variable to adjust the position of the bomb

    struct ObstacleStruct
    {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
        public int parentTileNum;
    }
    private ObstacleStruct[] obss;


    //The variables and methods for adding coins are the same as TileStruct.
    public GameObject Coin_money;
    private int coinNum = 5;
    struct CoinStruct
    {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
        public int parentTileNum;
    }
    private CoinStruct[] coin;


    private Vector3 tempVec; //Temporary vector value
    private Vector3 tileCenterVec; //Position value of the first tile
    private Vector3 wallPos; //Variable for storing wall location

    private int lastTileNum = 0; //Number with the last tile
    private Vector3 wallRo; //Vector Variables for the Wall


    public static NetworkMapCreate instance; //Set it to call itself in another script.


    private PlayerMove PM;
    private NetworkConsole soc;
    private float health;



    public bool Bomb_Update = false; //Variables for checking the creation of a bomb through vote


    void Awake()
    {
        tileCenterVec = new Vector3(-1.2f, -0.3f, 0);
        instance = this;
        CreateTiles();
        obsNum = 0;
        /* Sets the default location of the tile and puts its own value in the instance.
         * After creating a tile, set the number of traps to zero.
         */

    }
    void Start () {
        soc = FindObjectOfType<NetworkConsole>();
        PM = FindObjectOfType<PlayerMove>();
        health = PM.HP;
        //After saving each script's information, we save the HP value in PlayerMove to the health variable.
    }



    void Update()
    {
        if (Bomb_Update)
        {
            Bomb_Update = false;
            Make_Bomb();
        }
        //If Bomb_Update has a true value through voting, it will generate a bomb.
    }







    void CreateTiles()
    {
        // Create tiles.

        tempVec = tileCenterVec; // Initial reference point for positioning tile creation


        tiles = new TileSturct[tile_row, tile_column]; // Repeat tile_row * tile_column total times.
        obss = new ObstacleStruct[100]; // The maximum number of bombs is created by the number of tiles.
        coin = new CoinStruct[coinNum]; // The number of coins is only set to a preset value.
        Walls = new WallStruct[2]; // Create a wall to keep the player on the tile.


        wallRo = new Vector3(0, 0, 90);
        wallPos.y += tileGap;
        wallPos.x = tempVec.x - tileGap / 2;
        Walls[0].obj = Instantiate(Wall, position: wallPos, rotation: Quaternion.Euler(wallRo)) as GameObject;
        wallPos.x -= (tempVec.x * 3) + tileGap / 2;
        Walls[1].obj = Instantiate(Wall, position: wallPos, rotation: Quaternion.Euler(wallRo)) as GameObject;
        // After rotate the wall prefab, specify the position of each, and actually create it.


        for (int j = 0; j < tile_column; j++)
        {
            for (int i = 0; i < tile_row; i++)
            {
                // Set basic information and location.
                tiles[i, j].obj = Instantiate(tile, position: tempVec, rotation: Quaternion.identity) as GameObject;
                tiles[i, j].obj.GetComponent<TileInfo>().x = j;
                tiles[i, j].obj.GetComponent<TileInfo>().y = i;
                tiles[i, j].tf = tiles[i, j].obj.transform;
                tiles[i, j].pos = tiles[i, j].tf.position;
                tiles[i, j].active = true;

                tempVec.x += tileGap; // (Horizontal row) Next tile is created at the position moved by tileGapX
                lastTileNum++; // Sets the value of the variable to determine which is the last tile.

            }
            tempVec.y -= tileGap;
            tempVec.x = -1.2f; // After moving the column down one column, move the created position back to the initial row position.
        }



        while (coinNum > 0)
        {
            r = Random.Range(0, tile_row - 1);
            l = Random.Range(0, tile_column - 1);
            // Set random locations to create coins in random places.
            if (tiles[r, l].active == false)
            {
                continue; // If you already have a coin or bomb in a random location, run it again.
            }
            tiles[r, l].active = false;
            // If it's ok to create it, change the active tile to false.
            // This is to avoid duplicate creation.

            coin[coinNum - 1].obj = Instantiate(Coin_money, position: tiles[r, l].pos, rotation: Quaternion.identity) as GameObject;
            coin[coinNum - 1].tf = tiles[r, l].obj.transform;
            coin[coinNum - 1].pos = tiles[r, l].tf.position;
            coin[coinNum - 1].active = true;
            coin[coinNum - 1].parentTileNum = lastTileNum;
            coinNum--;
            // After setting up to create a coin, reduce the number you need to create one by one.
        }
    }

    public void vote_insert() //This is a variable that can be called from another script (like NetworkConsole).
    {
        Debug.Log("시청자들이 폭탄을 생성했어!");
        Bomb_Update = true;
    }



    public void Make_Bomb()
    {
        r = Random.Range(0, tile_row - 1);
        l = Random.Range(0, tile_column - 1);
        if (tiles[r, l].active != false)
        {
            tiles[r, l].active = false;
            obss[obsNum + 1].obj = Instantiate(obstacle, position: tiles[r, l].pos, rotation: Quaternion.identity) as GameObject;
            obss[obsNum + 1].tf = tiles[r, l].obj.transform;
            obss[obsNum + 1].pos = tiles[r, l].tf.position;
            obss[obsNum + 1].active = true;
            obss[obsNum + 1].parentTileNum = lastTileNum;
            obsNum++;
        }
    }
    //Generate bombs in the same way as coins.

    IEnumerator ServerMessage()
    {
        string rcvData;
        while (health > 0)
        {
            //send message...
            rcvData = soc.receiveFromServer();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
