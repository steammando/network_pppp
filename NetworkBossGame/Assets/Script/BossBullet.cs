using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    // Use this for initialization
    private int numOfBullet;
    private float speed = 200;
    private float shootGunSpeed = 500;
    private bool bulletCoolDown;
    private float direction;
    public GameObject bullet;
    void OnEnable()
    {
        numOfBullet = 10;
        bulletCoolDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
            StartCoroutine("SuperShoot");
    }
    public void shootBullet()
    {
        bulletCoolDown = false;
        StartCoroutine("oneShoot");
    }
    IEnumerator SuperShoot()
    {
        Debug.Log("Shoot");
        Vector3 playerPos;
        Vector3 bossPos;
        Vector3 shootDirection;

        for (int i=0;i<3;i++)
        {
            playerPos = GameManager.instance.player.transform.position;
            bossPos = GameManager.instance.boss.transform.position;
            shootDirection = playerPos - bossPos;
            shootDirection /= Mathf.Sqrt(Mathf.Pow(shootDirection.x, 2) + Mathf.Pow(shootDirection.y, 2));
            GameObject obj;
            obj = (GameObject)Instantiate(bullet, GameManager.instance.boss.transform.position, Quaternion.identity);

            obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(shootGunSpeed * shootDirection.x, shootGunSpeed * shootDirection.y, 0));
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
    IEnumerator oneShoot()
    {
        float angle = 120 / numOfBullet;
        /* do
         {*/
        for (int i = 0; i < numOfBullet; i++)
        {
            GameObject obj;
            obj = (GameObject)Instantiate(bullet, GameManager.instance.boss.transform.position, Quaternion.identity);

            //보스의 위치에 bullet을 생성합니다.
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / numOfBullet), speed * Mathf.Sin(Mathf.PI * i * 2 / numOfBullet)));
            
           
            obj.transform.Rotate(new Vector3(0f, 0f, 360 * i / numOfBullet - 90));

            // yield return new WaitForSeconds(0.1f);
        }
        
        for (int i = 0; i < numOfBullet; i++)
        {
            GameObject obj;
            obj = (GameObject)Instantiate(bullet, GameManager.instance.boss.transform.position, Quaternion.identity);

            //보스의 위치에 bullet을 생성합니다.
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * Mathf.Cos(Mathf.PI * 2 * i / numOfBullet), speed * Mathf.Sin(Mathf.PI * i * 2 / numOfBullet)));

            obj.transform.Rotate(new Vector3(0f, 0f, 360 * i / numOfBullet - 90));

             yield return new WaitForSeconds(0.1f);
        }
       
        GameManager.instance.boss.patternBoolean[0] = true;
        GameManager.instance.boss.GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(4f);
        bulletCoolDown = true;
        //} while (true) ;
    }
}
