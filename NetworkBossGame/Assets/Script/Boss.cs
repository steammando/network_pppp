using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public Image healthBar;

    //pattern bool --> each boolean value is mapping at boss's pattern
    public bool[] patternBoolean;
    public int nextPattern;

    //variable of boss...
    private float health;
    private float fullHealth;

    //TCP Sockt. Connection
    private SocketCon soc;
    private GameObject obj_pet;

    private Rigidbody2D rb2d;
    private Animator anim;

    //Pattern of boss
    public GameObject thorn_icile;
    private GameObject rightFist;
    private BossBullet bulletManager;
    private Thone thornManager;
    public GameObject pet;

    //Moving variables --> __now not used__
    //Boss moving direction 
    private bool direction;//
    //Boss move speed //
    private float moveSpeed = 10;//
    //Boss moving? yes:no;
    private bool activeBool;//

    //voteprimaryKey
    private int primaryKey = 100;
    // Use this for initialization
    void Start()
    {
        //___initialize___
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletManager = FindObjectOfType<BossBullet>();
        thornManager = FindObjectOfType<Thone>();
        soc = FindObjectOfType<SocketCon>();
        rightFist = GameObject.FindGameObjectWithTag("RightFist");
        //health is set...
        fullHealth = 1000;
        health = 1000;

        direction = true;
        activeBool = true;
        patternBoolean = new bool[5];
        for (int i = 0; i < 5; i++)
            patternBoolean[i] = false;
        //start corutine --> basic pattern...(while(true))
        StartCoroutine("DropThorn");
        StartCoroutine("BotThorn");
        StartCoroutine("VoteProcess");
        StartCoroutine("bossPet");
        nextPattern = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Boss") || anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            if (patternBoolean[0])
            {
                patternBoolean[0] = false;
                StartCoroutine("ShootBullet");
            }
            if (patternBoolean[1])
            {
                patternBoolean[1] = false;
                StartCoroutine("ReadyBeam");

            }
            if (patternBoolean[2])
            {
                patternBoolean[2] = false;
                rightFist.GetComponent<Fist>().bump();
            }
        }
    }

    //when collision with object...
    void OnTriggerEnter2D(Collider2D _col)
    {
        //if boss is collide with UserBullet...
        if (_col.tag == "UserBullet")
        {
            //Damaged Animation start && corutine...
            anim.SetTrigger("Damaged");
            StartCoroutine("Damaged");
        }
    }
    //drop thorn from top...
    void dropThorn()
    {
        Vector3 position = GameManager.instance.player.transform.position;
        position.y = 5f;
        position.x = position.x + Random.Range(-0.5f, 0.5f);
        Instantiate(thorn_icile, position, Quaternion.identity);
    }
    //set pattern bool true --> active corresponding pattern...
    public void PatternValid(int patternNum)
    {
        //Debug.Log(patternNum);
        //patternQueue.Enqueue(patternNum);
        patternBoolean[patternNum] = true;
        //thoneManager.stab();
    }

    //got message from server.
    //but current version, Socket manager processing recive packet
    IEnumerator ServerMessage()
    {
        string rcvData;
        while (health > 0)
        {
            //send message...
            rcvData = soc.receiveFromServer();
            yield return new WaitForSeconds(0.5f);
        }
    }
    //start vote...per every 11 seconds.
    //If you do not give time between, it will stick.
    IEnumerator VoteProcess()
    {
        while (true)
        {
            if (health < 0)
                break;

            soc.sendToServer(VoteManager.instance.startVote(primaryKey, 1, 9));
            GameManager.instance.socket_boolean = false;
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.setVoteName(primaryKey, "Attack pattern"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(primaryKey, 1, "Rage"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(primaryKey, 2, "Beam"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(primaryKey, 3, "Bump"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(primaryKey));
            primaryKey++;
            yield return new WaitForSeconds(11f);
        }
        yield return null;
    }
    //shoot bullet using bullet manager.
    IEnumerator ShootBullet()
    {
        patternBoolean[0] = false;//(if shoot bullet active...)
        //set animation...
        anim.SetTrigger("BulletReady");
        yield return new WaitForSeconds(1f);
        //shoot bullet --> bullet manager's function call.
        bulletManager.shootBullet();
        yield return null;
    }
    //drop thorn pattern... --> loop(true)
    //called each 3f
    IEnumerator DropThorn()
    {
        while (true)
        {
            dropThorn();
            yield return new WaitForSeconds(3f);
        }
    }
    //stab thorn from floor. called each 5f(loop (true)
    IEnumerator BotThorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            thornManager.stab();
            yield return new WaitForSeconds(3f);
        }
    }
    //shoot beam pattern. need 3 animation...
    IEnumerator ReadyBeam()
    {
        anim.SetTrigger("Ready");
        patternBoolean[1] = false;
        while (true)
        {
            //if(animation process >0.7...)
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlinkEye") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
            {
                //set next animation
                anim.SetTrigger("Shoot");

                GameManager.instance.beam.ShootBeam();
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    //when boss damaged, set damaged motion
    IEnumerator Damaged()
    {
        health -= 10;
        //gui health bar updated
        healthBar.fillAmount = health / fullHealth;
        while (true)
        {
            //if(current animation is damaged, the procession is >=80%)
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
    //Motion related functions are not used!
    //not used on current version...(boss not move!)
    IEnumerator move()
    {
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
    IEnumerator bossPet()
    {
        while (true)
        {
            Vector3 Ppos = GameManager.instance.player.transform.position;
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

            yield return new WaitForSeconds(5f);
        }
    }
    public bool validInput_move()
    {
        if (rb2d.velocity.x != 0)
            return false;
        return true;
    }
    //set health of boss...(not used on current version.)
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
