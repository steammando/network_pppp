using UnityEngine;
public class MapCreate : MonoBehaviour {


    public GameObject tile;
    public GameObject UnderGround;
    public GameObject Wall;
    private int tile_row = 5; // 가로
    private int tile_column = 20; // 세로


    public struct WallStruct {
        public GameObject obj;
        public Transform tf; // 위치 변환용
        public bool active;
        public Vector3 pos; //위치 정보 저장용
    }
    public WallStruct[] Walls;





    public struct TileSturct {
        public GameObject obj;
        public Transform tf; // 위치 변환용
        public bool active;
        public Vector3 pos; //위치 정보 저장용
    }
    public TileSturct[,] tiles; //바닥에 사용할 GameObject(프리팹) 가로 * 세로
    private float tileGap = 0.65f;


    //함정 추가
    public GameObject obstacle;
    private int obsNum = 17; //Random.Range(1,3);
    private int r, l;



    struct ObstacleStruct
    {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
        public int parentTileNum;
    }
    private ObstacleStruct[] obss;




    //코인 추가
    public GameObject Coin_money;
    private int coinNum = 5;
    struct CoinStruct {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
        public int parentTileNum;
    }
    private CoinStruct[] coin;




    private Vector3 tempVec; // 임시벡터값
    private Vector3 tileCenterVec; // 최초 타일의 위치값
    private Vector3 wallPos; // 벽 위치 저장용

    private int lastTileNum = 0;
    private Vector3 wallRo;

    void Awake()
    {
        tileCenterVec = new Vector3(-1.2f,-0.3f,0);
        CreateTiles(); // 타일과 장애물 생성
    }



    void CreateTiles()
    { //사용할 타일들을 생성합니다.
        tempVec = tileCenterVec; // 타일 생성 위치 지정을 위한 최초 기준점
        //RightWall = Instantiate(RightWall, position: , rotation: Quaternion.identity) as GameObject;

        tiles = new TileSturct[tile_row, tile_column]; // 총 tile_row * tile_column 만큼을 반복해서 사용합니다.
        obss = new ObstacleStruct[obsNum];
        coin = new CoinStruct[coinNum];
        Walls = new WallStruct[2];


        wallRo = new Vector3(0, 0, 90);
        wallPos.y += tileGap;
        wallPos.x = tempVec.x - tileGap/2;
        Walls[0].obj = Instantiate(Wall, position: wallPos, rotation: Quaternion.Euler(wallRo)) as GameObject;
        wallPos.x -= (tempVec.x * 3) + tileGap/2;
        Walls[1].obj = Instantiate(Wall, position: wallPos, rotation: Quaternion.Euler(wallRo)) as GameObject;


        for (int j = 0; j < tile_column; j++)
        {
            for (int i = 0; i < tile_row; i++)
            {
                //기본 정보와 위치를 셋팅해줍니다.
                
                tiles[i, j].obj = Instantiate(tile, position: tempVec, rotation: Quaternion.identity) as GameObject;
                tiles[i, j].obj.GetComponent<TileInfo>().x = j;
                tiles[i, j].obj.GetComponent<TileInfo>().y = i;
                tiles[i, j].tf = tiles[i, j].obj.transform;
                tiles[i, j].pos = tiles[i, j].tf.position;
                tiles[i, j].active = true;
        

                
                

                tempVec.x += tileGap; // (가로행) 다음 타일은 tileGapX만큼 이동한 위치에 생성
                lastTileNum++; // 현재 마지막 타일이 어느 것인지 판별하기 위한 변수++

            }
            tempVec.x = -1.2f; // 다시 초기 위치로 이동;
            tempVec.y -= tileGap; //(세로행) 다음 타일은 tileGapY만큼 이동한 위치에서부터 생성하기 시작한다.
        }



        while (obsNum > 0)
        {
            r = Random.Range(0, tile_row);
            l = Random.Range(0, tile_column);

            if(tiles[r,l].active == false)
            {
                continue;
            }
            tiles[r, l].active = false;
            obss[obsNum - 1].obj = Instantiate(obstacle, position: tiles[r,l].pos, rotation: Quaternion.identity) as GameObject;
            obss[obsNum - 1].tf = tiles[r, l].obj.transform;
            obss[obsNum - 1].pos = tiles[r, l].tf.position;
            obss[obsNum - 1].active = true;
            obss[obsNum - 1].parentTileNum = lastTileNum;

            obsNum--;
        }
        while(coinNum > 0)
        {
            r = Random.Range(0, tile_row - 1);
            l = Random.Range(0, tile_column - 1);
            if (tiles[r, l].active == false)
            {
                continue;
            }
            tiles[r, l].active = false;

            coin[coinNum - 1].obj = Instantiate(Coin_money, position: tiles[r, l].pos, rotation: Quaternion.identity) as GameObject;
            coin[coinNum - 1].tf = tiles[r, l].obj.transform;
            coin[coinNum - 1].pos = tiles[r, l].tf.position;
            coin[coinNum - 1].active = true;
            coin[coinNum - 1].parentTileNum = lastTileNum;
            coinNum--;
        }
    }

}
