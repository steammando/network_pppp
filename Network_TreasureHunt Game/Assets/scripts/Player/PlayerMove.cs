using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public int HP = 10;
    public int Money = 0;
    public float speed = 8f;
    public float movePower = 1f;


    Rigidbody2D rigid;
    Vector3 movement;


    private Rigidbody2D rb2d;
    private Transform playerTF;
    private Vector3 playerPos;
    private Vector3 TempPlayerScale;
    
    private TileInfo currentTile = null;

    private void Awake()
    {
        playerTF = transform;
        rb2d = GetComponent<Rigidbody2D>();
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

   
    void Start () {
        playerPos = playerTF.position;
        rigid = gameObject.GetComponent<Rigidbody2D>();
	}
	

	void Update () {
        acting();
    }
    private void FixedUpdate()
    {
        Move();
    }





    void OnCollisionStay2D(Collision2D col)
    {
        int j = col.gameObject.GetComponent<TileInfo>().x;
        int i = col.gameObject.GetComponent<TileInfo>().y;
        currentTile = col.gameObject.GetComponent<TileInfo>();
    }
    
    public void acting()
    {
        
        if (Input.GetKey(KeyCode.Space) == true) // dig tile
        {
            if(currentTile!=null)
                GameObject.Destroy(currentTile.gameObject);
        }
        
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
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
