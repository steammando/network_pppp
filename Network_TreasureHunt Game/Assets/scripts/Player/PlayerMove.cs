using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public int HP = 10;
    public int Money = 0;
    public float speed = 8f;





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

        if (Input.GetKeyDown(KeyCode.LeftArrow) == true) // Move left
        {

            /*if()
                rb2d.velocity = new Vector3(speed, 0, 0);*/
                
                playerPos.x -= speed;

            /*if (gameObject.tag == "Block"){
                Destroy(gameObject);

                //gameObject.active = false;

                //충돌도 같이하면 부숴라

            }*/

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) == true) // Move Right
        {
            playerPos.x += speed;
            rb2d.velocity = new Vector3(speed, 0, 0);
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) == true) // dig tile
        {
            playerPos.y -= (1f /speed);
            
        }
        gameObject.transform.position = playerPos;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        int j = col.gameObject.GetComponent<TileInfo>().x;
        int i = col.gameObject.GetComponent<TileInfo>().y;
        col.gameObject.GetComponent<TileInfo>().check_active();
    }




    public void Have_Damage(int damage)
    {
        HP -= damage;
    }
    public void Earn_Money(int gold)
    {
        Money += gold;
    }
  




    public void TurnThePlayer(int type) //살짝 넉백 주기
    {
        if (type == 1) //오른쪽 벽과 충돌
        {
            playerPos.x -= 0.2f;
        }
        if (type == 2) //왼쪽 벽과 충돌
        {
            playerPos.x += 0.2f;
        }
    }
}
