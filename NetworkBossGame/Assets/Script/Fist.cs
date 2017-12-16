using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{

    public bool isLeft;
    public GameObject TangnamBall;

    private bool atking;
    private bool palm;
    private GameObject temp;
    private Rigidbody2D rb2d;
    private Animator anim;
    enum state
    {
        attack, idle
    };
    private Vector3 bossPosition;
    private Vector3 tempPosition;
    private Vector3 objectPosition;
    private state currentState;

    // Use this for initialization
    void Start()
    {
        currentState = state.idle;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        atking = false;
        palm = true;
    }

    // Update is called once per frame
    void Update()
    {
        bossPosition = GameManager.instance.boss.transform.position;
        if (currentState == state.idle)
        {
            objectPosition = bossPosition;
            if (isLeft)
            {
                
            }
            else
            {
                objectPosition.y = -3f;
                objectPosition.x += 3f;
                gameObject.transform.position = objectPosition;
            }
        }
    }

    public void bump()
    {
        GameManager.instance.boss.patternBoolean[2] = false;
        anim.SetTrigger("Bump");
    }
    
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("MainFloor")&&!isLeft)
        {
            atking = true;
            StartCoroutine("TangNam");
        }
        if (_col.CompareTag("MainFloor") && isLeft)
            atking = true;
        if (_col.CompareTag("Player") && atking)
            GameManager.instance.player.Damaged(10);
    }
    void OnTriggerStay2D(Collider2D _col)
    {
        if (_col.CompareTag("Player") && atking)
            GameManager.instance.player.Damaged(10);
    }
    IEnumerator Palm()
    {
        yield return null;
    }
    IEnumerator TangNam()
    {
        for (int i = 0; i < 3; i++)
        {
            temp = Instantiate(TangnamBall, gameObject.transform.position, Quaternion.identity);
            temp.GetComponent<TangnamBall>().AddForce(Random.Range(0, 2));
        }
        yield return new WaitForSeconds(0.3f);
        atking = false;
        yield return null;
    }
    void OnTriggerExit2D(Collider2D _col)
    {
        if (_col.CompareTag("MainFloor")&&!isLeft)
        {
            //GameManager.instance.boss.patternBoolean[2] = true;
            anim.ResetTrigger("Bump");
            anim.SetTrigger("Idle");
        }
    }
}
