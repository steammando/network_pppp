using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject thorn_icile;
    public GameObject pet;
    public bool[] patternBoolean;

    private GameObject obj_pet;
    private int health;
    private float moveSpeed = 10;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool activeBool;
    private GameObject rightFist;
    private BossBullet bulletManager;
    private Thone thoneManager;
    bool direction;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletManager = FindObjectOfType<BossBullet>();
        thoneManager = FindObjectOfType<Thone>();

        rightFist = GameObject.FindGameObjectWithTag("RightFist");
        direction = true;
        activeBool = true;
        patternBoolean = new bool[5];
        for (int i = 0; i < 5; i++)
            patternBoolean[i] = true;
        StartCoroutine("DropThorn");
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Boss")|| anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && patternBoolean[0])
            {
                StartCoroutine("ShootBullet");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && patternBoolean[1])
            {
                StartCoroutine("ReadyBeam");

            }
            if(Input.GetKeyDown(KeyCode.Alpha3)&&patternBoolean[2])
            {
                rightFist.GetComponent<Fist>().bump();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && patternBoolean[3])
        {
            thoneManager.stab();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)&&patternBoolean[4])
        {
            Vector3 Ppos = GameManager.instance.player.transform.position;
            patternBoolean[4] = false;
            if (Ppos.x < 0)
            {
                Ppos.x = 10;
                Ppos.y -= 1f;
                obj_pet = Instantiate(pet, Ppos, Quaternion.identity);
                obj_pet.GetComponent<BossPet>().run(-1);
            }
            else
            {
                Ppos.x = -10;
                Ppos.y -= 1f;
                obj_pet = Instantiate(pet, Ppos, Quaternion.identity);
                obj_pet.GetComponent<BossPet>().run(1);
            }
        }
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
    IEnumerator ShootBullet()
    {
        patternBoolean[0] = false;
        anim.SetTrigger("BulletReady");
        yield return new WaitForSeconds(1f);
        bulletManager.shootBullet();
        yield return null;
    }
    IEnumerator DropThorn()
    {
        while (true)
        {
            dropThorn();
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator ReadyBeam()
    {
        anim.SetTrigger("Ready");
        patternBoolean[1] = false;
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlinkEye") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                anim.SetTrigger("Shoot");

                GameManager.instance.beam.ShootBeam();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    IEnumerator Damaged()
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
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
        if (rb2d.velocity.x != 0)
            return false;
        return true;
    }
    public void setHealth(int difficult)
    {
        switch (difficult)
        {
            case 1:
                health = 1000;
                break;
            case 2:
                health = 2000;
                break;
            case 3:
                health = 3000;
                break;
            default:
                health = 500;
                break;
        }
    }
}
