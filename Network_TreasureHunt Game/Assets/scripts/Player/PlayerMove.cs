using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour {
    public float HP = 10;
    public float MaxHP = 10;
    public int Money = 0;
    public float movePower = 1.5f;
    public float HP_gage;
    public Text Current_Money = null;
    public int level = 1;



    Rigidbody2D rigid;

    private Rigidbody2D rb2d;
    private Transform playerTF;
    private Vector3 playerPos;
    
    private TileInfo currentTile = null; // Save the current tile
    private VendingMachineInfo vm = null;
    public static PlayerMove instance;
    private void Awake()
    {
        instance=this;
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
        if (col.gameObject.CompareTag("Block")) {
            int j = col.gameObject.GetComponent<TileInfo>().x;
            int i = col.gameObject.GetComponent<TileInfo>().y;
            currentTile = col.gameObject.GetComponent<TileInfo>();
        }
        else if (col.gameObject.CompareTag("Ledder"))
        {
            GameClear();
        }
        else if (col.gameObject.CompareTag("VendingMachine"))
        {
            vm = col.gameObject.GetComponent<VendingMachineInfo>();
        }
    }
    
    public void acting()
    {
        
        if (Input.GetKey(KeyCode.Space) == true) // dig tile
        {
            if(currentTile!=null)
                GameObject.Destroy(currentTile.gameObject); // Delete conflict checked tiles.
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
    public void Have_Damage(float damage)
    {
        HP -= damage;
        HP_gage = HP * 0.1f;
        GameObject.Find("Canvas").transform.Find("HP_gage").gameObject.GetComponent<Image>().fillAmount = HP_gage;
        
        if(HP <= 0)
        {
            Debug.LogError("게임 오버");
            NetworkConsole.instance.endSocketCon();
            GameObject.Find("Canvas").transform.Find("GameoverPanel").gameObject.SetActive(true); //Activate game over panel
            // can not find the gameoverpanel because I can not find the gameoverpanel because the current state of the gameovepanel is inactive.
          
            Time.timeScale = 0; // time stop
        }

    }
    public void Earn_Money(int gold)
    {
        Money += gold;
        string MM = "Money : " + Money.ToString();
        GameObject.Find("Canvas").transform.Find("Money_info").gameObject.GetComponent<Text>().text = MM;
    }

    public void GameClear()
    {
        NetworkConsole.instance.endSocketCon();
        Debug.LogError("게임 클리어");
        GameObject.Find("Canvas").transform.Find("GameClearPanel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void BuyPortion(int portionPrice, float portionValue)
    {
        if (portionPrice <= Money && (int)HP_gage*10 <= MaxHP)
        {   
            Debug.Log("돈이 충분해!");
            SoundManager.soundManager.PlayHPrecoverSound();
            if ((int)HP_gage * 10 <= MaxHP)
            {
                HP += portionValue;
            }
            Money -= portionPrice;
            HP_gage = HP * 0.1f;
            GameObject.Find("Canvas").transform.Find("HP_gage").gameObject.GetComponent<Image>().fillAmount = HP_gage;
            string MM = "Money : " + Money.ToString();
            GameObject.Find("Canvas").transform.Find("Money_info").gameObject.GetComponent<Text>().text = MM;

        }
        else
        {
            Debug.Log("돈이 부족하거나, 체력이 만땅이야!");
        }

    }
}
