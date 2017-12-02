using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject knife;

    private Rigidbody2D rb2d;
    private Animator anim;

    private Vector3 moveVector;
    private Vector3 SpeedVector;
    private GameObject temp;
    private Color color;

    private float health=100;
    private float moveSpeed = 5f;
    private bool invincibility;
    private bool activeBool;
    private bool attackState;
    private bool attackFlag;
    private bool keyActivation;
   
    private float direction;
    private enum state
    {
        ground,air
    };
    state curPos;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeBool = false;//activeBool은 현재 지정된 키를 입력하고 있는지 확인하는 부분.
        invincibility = false;
        attackState = false;
        attackFlag = true;
        curPos = state.air;
	}

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(curPos);
        direction = gameObject.transform.localScale.x;
        if (rb2d.velocity.y < 0)
        {
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Move");
            curPos = state.air;
            anim.SetTrigger("Down");
        }
        if(keyActivation)
            StartCoroutine("Move");
        
        attackState = isAttack();
        
        if (curPos==state.ground&&!activeBool)
        {
            anim.SetTrigger("Idle");
            moveVector = new Vector3(0, rb2d.velocity.y, 0);
            rb2d.velocity = moveVector;
            
        }
        else if (activeBool)
        {
            //anim.ResetTrigger("Idle");
            moveVector.y = rb2d.velocity.y;
            rb2d.velocity = moveVector;
            activeBool = false;
        }

    }

    public void Damaged(int _d)
    {
        if (!invincibility)
        {
            StartCoroutine("MakeInv");
            keyActivation = false;
            health -= _d;

            anim.SetTrigger("Jump");
            anim.SetTrigger("Down");
            Debug.Log(direction);
            rb2d.velocity = new Vector3(0, 0, 0);
            curPos = state.air;
            rb2d.AddForce(new Vector3(direction * -100f, 100f, 0));
            Debug.Log("HP : " + health);
            if (health <= 0)
                GameManager.instance.GameOver();
        }
    }
    IEnumerator MakeInv()
    {
        invincibility = true;
        for(int i=0;i<5;i++)
        {
            color = gameObject.GetComponent<SpriteRenderer>().color;
            if (color.a == 255f)
                color.a = 0f;
            else
                color.a = 255f;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
        invincibility = false;
        color.a = 255;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        yield return null;
    }
    IEnumerator Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            if (curPos == state.ground)
                anim.SetTrigger("Move");
            activeBool = true;
            moveVector.x = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
            if(curPos==state.ground)
                 anim.SetTrigger("Move");
            activeBool = true;
            moveVector.x = moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.UpArrow) && curPos==state.ground)
        {
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Attack");
            anim.ResetTrigger("Move");

            anim.SetTrigger("Jump");
            anim.SetTrigger("Down");
            curPos = state.air;
            activeBool = true;
            
            rb2d.AddForce(new Vector3(0, 300f, 0));
        }

        if(Input.GetKey(KeyCode.D) && curPos==state.ground)
        {
            anim.SetTrigger("Attack");
            StartCoroutine("shootBullet");
            moveSpeed = 1f;
        }
        yield return null;
    }
    IEnumerator shootBullet()
    {
        while (true)
        {
            //애니메이션이 60%진행 되었을 때
            if (attackFlag && anim.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
            {
                temp = Instantiate(knife, gameObject.transform.position, Quaternion.identity);
                attackFlag = false;//flag -> false.
                //발사
                Knife shooted_Bullet = temp.GetComponent<Knife>();
                shooted_Bullet.shoot((int)gameObject.transform.localScale.x);
                break;   
            }
            yield return new WaitForSeconds(0.5f);
        }
        //코루틴 종료
        yield return null;
    }
    bool isAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            anim.ResetTrigger("Attack");
            return true;
        }
        else
        {
            attackFlag = true;
            moveSpeed = 5f;
            return false;
        }
    }

    void OnCollisionStay2D(Collision2D _col)
    {
        if ((_col.gameObject.tag == "Floor" || _col.gameObject.tag == "MainFloor" )&& curPos==state.ground)
        {
            keyActivation = true;
            anim.ResetTrigger("Jump");
            anim.ResetTrigger("Down");
            rb2d.velocity = new Vector3(0, rb2d.velocity.y, 0);

            curPos = state.ground;
        }
        if(_col.gameObject.tag=="Floor" && Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 tmp=gameObject.transform.position;
            tmp.y -= 0.5f;
            gameObject.transform.position = tmp;
        }
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Floor"||_col.gameObject.tag=="MainFloor")
        {
            Debug.Log("Endl!");
            anim.ResetTrigger("Jump");
            anim.ResetTrigger("Down");
            anim.ResetTrigger("Move");

            anim.SetTrigger("Idle");
            //moveVector.x = 0f;
            rb2d.velocity = new Vector3(0,0,0);
            
            curPos = state.ground;
        }
        if (_col.gameObject.tag == "BossBullet")
        {
            Debug.Log("Damaged!");
            Damaged(5);
        }
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("BossBullet"))
        {
            Debug.Log("Damaged!");
            Damaged(5);
        }
    }
    public void setHealth()
    {
        health = GameManager.instance.getDifficult()*50;
    }
    public void setAttackFlag()
    {
        attackFlag = true;
    }
}
