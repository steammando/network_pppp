﻿using System.Collections;
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
        nextPattern = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
            soc.sendToServer("Vote_");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Boss")|| anim.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
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
            if(patternBoolean[2])
            {
                patternBoolean[2] = false;
                rightFist.GetComponent<Fist>().bump();
            }
        }
        if(patternBoolean[4])
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
   
    IEnumerator ServerMessage()
    {
        string rcvData;
        while (health>0)
        {
            rcvData = soc.receiveFromServer();

            Debug.Log(rcvData);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator VoteProcess()
    {
        while (true)
        {
            if (health < 0)
                break;

            soc.sendToServer(VoteManager.instance.startVote(100, 1, 11));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(100, 0, "Rage"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(100, 1, "Beam"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(100, 2, "Bump"));
            yield return new WaitForSeconds(0.2f);
            soc.sendToServer(VoteManager.instance.VoteEntry(100));
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
    IEnumerator DropThorn()
    {
        while (true)
        {
            dropThorn();
            yield return new WaitForSeconds(3f);
        }
    }
    IEnumerator BotThorn()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            thornManager.stab();
            yield return new WaitForSeconds(3f);
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
        health -= 10;
        healthBar.fillAmount = health / fullHealth;
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
    //set health of boss...
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
