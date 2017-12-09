using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour {

    float xHalfSize;
    float yHalfSize;

    public int x;
    public int y;
    public bool active;

    Vector3 thisBlockPos;
    Vector3 tempPlayerPos;
    Vector3 TempPlayerScale;
    static float playerSide = 0.4f;

    private void Awake()
    {
        thisBlockPos = transform.position;

        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
        xHalfSize = transform.localScale.x * (tempCol.size.x * 0.5f);
        yHalfSize = transform.localScale.y * (tempCol.size.y * 0.5f);
        tempCol = null;

    }

    float shortestDist;
    int shortestSide = 0;
    float tempDist;


    void CheckCollisionSide()
    {
        shortestDist = Mathf.Abs(tempPlayerPos.x + (TempPlayerScale.x * playerSide) - thisBlockPos.x - xHalfSize);
        shortestSide = 1; //기본은 오른쪽 충돌
        tempDist = Mathf.Abs(tempPlayerPos.x - (TempPlayerScale.x * playerSide) - thisBlockPos.x + xHalfSize);

        if(shortestDist > tempDist)
        {
            shortestDist = tempDist;
            shortestSide = 2; //왼쪽 충돌
        }
    }

    // Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("tile[" + y + "," + x + "]과 충돌");
        active = false;

        /*
        tempPlayerPos = col.transform.position;
        TempPlayerScale = col.transform.localScale;

        CheckCollisionSide();
        if(shortestSide < 3)
        {
            if (shortestSide == 1)
            { // 우측 충돌
                col.gameObject.GetComponent<PlayerMove>().TurnThePlayer(2);
            }
            else
            { //좌측 충돌
                col.gameObject.GetComponent<PlayerMove>().TurnThePlayer(1);
            }
        }
        */

    }

    public void check_active()
    {
        if (active == false)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}


