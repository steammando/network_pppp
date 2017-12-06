using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {



    public int HP = 10;
    public int Money = 0;
    public float speed = 0.00002f;

    private Transform playerTF;
    private Vector3 playerPos;


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
       gameObject.transform.position = playerPos;

    }
    public void Have_Damage(int damage)
    {
        HP -= damage;
    }
}
