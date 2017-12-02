using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject thorn_icile;

    private int health;
    private float moveSpeed = 10;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool activeBool;
    
    bool direction;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = true;
        activeBool = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha3)&&activeBool)
        {
            direction = true;
            StartCoroutine("move");
        }
        if (Input.GetKey(KeyCode.Alpha1)&&activeBool)
        {
            direction = false;
            StartCoroutine("move");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && activeBool)
            dropThorn();
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.tag == "UserBullet")
        {
            //Debug.Log("AAA");
            anim.SetTrigger("Damaged");
            StartCoroutine("Damaged");
        }
    }

    void dropThorn()
    {
        Vector3 position = GameManager.instance.player.transform.position;
        position.y = 5f;
        position.x = position.x + Random.Range(-0.5f, 0.5f);
        if (thorn_icile == null)
            Debug.Log("is NULL...");
        Instantiate(thorn_icile, position, Quaternion.identity);
    }

    IEnumerator Damaged()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged")&&anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                Debug.Log("set!");
                anim.ResetTrigger("Damaged");
                anim.SetTrigger("Idle");
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    IEnumerator move()
    {
        Debug.Log("aa");
        activeBool = false;
        if (validInput_move())
        {
            if (direction)
            {
                rb2d.velocity = new Vector3(moveSpeed, 0, 0);
            }
            if (!direction)
            {
                rb2d.velocity = new Vector3(-moveSpeed, 0, 0);
            }
        }
        yield return new WaitForSeconds(0.6f);
        rb2d.velocity = new Vector3(0, 0, 0);
        activeBool = true;
    }
    public bool validInput_move()
    {
        if (rb2d.velocity.x!=0)
            return false;
        return true;
    }
    public void setHealth(int difficult)
    {
       switch(difficult)
        {
            case 1:
                health = 100;
                break;
            case 2:
                health = 200;
                break;
            case 3:
                health = 300;
                break;
            default:
                health = 50;
                break;
        }
    }
}
