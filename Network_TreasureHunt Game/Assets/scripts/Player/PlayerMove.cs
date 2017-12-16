using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour {
    public float HP = 10;
    public float MaxHP;
    public int Money = 0;
    public float movePower = 1.5f;
    public float HP_gage;
    public Text Current_Money = null;
    public int level = 1;



    Rigidbody2D rigid;

    private Rigidbody2D rb2d;
    private Transform playerTF;
    private Vector3 playerPos;
    
    private TileInfo currentTile = null; //현재 닿아있는 tile을 저장
    private VendingMachineInfo vm = null;

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
                GameObject.Destroy(currentTile.gameObject); //충돌 체크된 타일을 삭제시킨다.
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
            GameObject.Find("Canvas").transform.Find("GameoverPanel").gameObject.SetActive(true); //게임 오버 패널 활성화
            /* 바로 gameoverpanel을 찾지 않고, canvas를 경유해서 찾는 이유는 현재 gameovepanel의 상태가 비활성화 상태이므로
             * 찾을 수가 없기 때문이다.
             */
            Time.timeScale = 0; //시간 멈춤
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
        Debug.LogError("게임 클리어");
        GameObject.Find("Canvas").transform.Find("GameClearPanel").gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void BuyPortion(int portionPrice, float portionValue)
    {
        if (portionPrice <= Money && HP < MaxHP)
        {   
            Debug.Log("돈이 충분해!");
            SoundManager.soundManager.PlayHPrecoverSound();
            if (HP < MaxHP)
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

    public void Next_Level()
    {
        playerTF = transform;
        playerPos = playerTF.position;
        level++;
        MaxHP += (int)Mathf.Log(level, 2f);

    }

}
