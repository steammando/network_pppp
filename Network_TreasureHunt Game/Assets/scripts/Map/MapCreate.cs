using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {
    public GameObject tile;
    private int tile_row = 5; // 가로
    private int tile_column = 20; // 세로

    struct TileSturct {
        public GameObject obj;
        public Transform tf; // 위치 변환용
        public bool active;
        public Vector3 pos; //위치 정보 저장용
    }

    private TileSturct[,] tiles; //바닥에 사용할 GameObject(프리팹) 가로 * 세로

    private float tileGap = 0.6f;


    //함정 추가
    public GameObject obstacle;
    private int obsNum = 10;
    struct ObstacleStruct
    {
        public GameObject obj;
        public Transform tf;
        public bool active;
        public Vector3 pos;
        public int parentTileNum;
    }
    private ObstacleStruct[] obss;





    private Vector3 tempVec; // 임시벡터값
    private Vector3 tileCenterVec; // 최초 타일의 위치값


    private int lastTileNum = 0;

    void Awake()
    {
        tileCenterVec = new Vector3(-1.2f,-0.3f,0);
        CreateTiles(); // 타일 생성
  
    }



    void CreateTiles()
    { //사용할 타일들을 생성합니다.

        tempVec = tileCenterVec; // 타일 생성 위치 지정을 위한 최초 기준점

        tiles = new TileSturct[tile_row, tile_column]; // 총 tile_row * tile_column 만큼을 반복해서 사용합니다.

        for (int j = 0; j < tile_column; j++)
        {
            for (int i = 0; i < tile_row; i++)
            {
                //기본 정보와 위치를 셋팅해줍니다.
                tiles[i, j].obj = Instantiate(tile, position: tempVec, rotation: Quaternion.identity) as GameObject;
                tiles[i, j].tf = tiles[i, j].obj.transform;
                tiles[i, j].pos = tiles[i, j].tf.position;
                tiles[i, j].active = true;

                tempVec.x += tileGap; // (가로행) 다음 타일은 tileGapX만큼 이동한 위치에 생성
                lastTileNum++; // 현재 마지막 타일이 어느 것인지 판별하기 위한 변수++

            }
            tempVec.x = -1.2f; // 다시 초기 위치로 이동;
            tempVec.y -= tileGap; //(세로행) 다음 타일은 tileGapY만큼 이동한 위치에서부터 생성하기 시작한다.
        }
    }



    void CreateObss ()
    { // 장애물을 생성해서, 타일과 위치를 랜덤하게 교환합니다.
        // 나중에는 난수로 배정하는게 아닌 투표를 통해서 위치 정보를 받아올 예정
        obss = new ObstacleStruct[obsNum];


    }
}
