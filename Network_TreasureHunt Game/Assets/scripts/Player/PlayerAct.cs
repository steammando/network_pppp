using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour {
    bool jumpOn = false; // 확인용
    float jumpPower = 0.3f;
    float tempJump;
    Transform playerTF;
    Vector3 tempVec;

    private void Awake()
    {
        playerTF = transform;
    }


    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if(!jumpOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                StartCoroutine("JumpAction");
            }
        }
	}
    IEnumerator JumpAction()
    { // 점프 처리는 코루틴으로.
        // 누르는 시간에 따라서 점프 파워가 달라진다.
        jumpOn = true;
        tempVec = playerTF.position;

        tempJump = jumpPower;
        tempVec.y += tempJump;
        playerTF.position = tempVec;

        while (tempVec.y>0)
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
