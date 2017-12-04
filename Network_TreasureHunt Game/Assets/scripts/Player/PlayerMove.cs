using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {



    public int HP = 10;
    public int Money = 0;
    public float speed = 0.00002f;

    private Transform playerTF;
    private Vector3 playerPos;


    bool jumpOn = false; // 확인용
    float jumpPower = 0.3f;
    float tempJump;
    Vector3 tempVec;



    private void Awake()
    {
        playerTF = transform;
    }

    // Use this for initialization
    void Start () {
        playerPos = playerTF.position;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.LeftArrow) == true) // Move left
        {
            playerPos.x -= speed;
        }

        if (Input.GetKey(KeyCode.RightArrow) == true) // Move Right
        {
            playerPos.x += speed;
        }
        if (!jumpOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                StartCoroutine("JumpAction");
            }
        }
        gameObject.transform.position = playerPos;

    }
    IEnumerator JumpAction()
    { // 점프 처리는 코루틴으로.
        // 누르는 시간에 따라서 점프 파워가 달라진다.
        jumpOn = true;
        tempVec = playerTF.position;

        tempJump = jumpPower;
        tempVec.y += tempJump;
        playerTF.position = tempVec;

        while (tempVec.y > 0)
        {
            yield return new WaitForSeconds(0.03f);
            tempJump -= 0.05f;
            tempVec.y += tempJump;
            playerTF.position = tempVec;
        }
        tempVec.y = 0.3f;
        playerTF.position = tempVec;
        jumpOn = false;
    }
}
