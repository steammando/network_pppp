using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public int HP = 10;
    public int Money = 0;
    public float speed = 8f;


    public bool dig_mode;

    private Rigidbody2D rb2d;
    private Transform playerTF;
    private Vector3 playerPos;
    private Vector3 TempPlayerScale;

    private void Awake()
    {
        playerTF = transform;
        rb2d = GetComponent<Rigidbody2D>();
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start () {
        playerPos = playerTF.position;
	}
	
	// Update is called once per frame
	void Update () {

        Moving();
    }
    
    void OnCollisionStay2D(Collision2D col)
    {
        int j = col.gameObject.GetComponent<TileInfo>().x;
        int i = col.gameObject.GetComponent<TileInfo>().y;
        col.gameObject.GetComponent<TileInfo>().check_active();
    }
    
    public void Moving()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true) // Move left
        {
            playerPos.x -= speed;
            Debug.Log("왼쪽 이동");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) == true) // Move Right
        {
            playerPos.x += speed;
            Debug.Log("오른쪽 이동");
        }




        if (Input.GetKey(KeyCode.Space) == true) // dig tile
        {

            dig_mode = true;
            Debug.Log("Dig == true");
        }
        else
        {
            dig_mode = false;
            Debug.Log("Dig == false");
        }
    }


    public void Have_Damage(int damage)
    {
        HP -= damage;
    }
    public void Earn_Money(int gold)
    {
        Money += gold;
    }
    
}
