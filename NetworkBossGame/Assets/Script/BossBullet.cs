using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    // Use this for initialization
    private int numOfBullet;
    private float speed = 200;
    private bool bulletCoolDown;
    private float direction;
    public GameObject bullet; 
	void OnEnable () {
        numOfBullet = 10;
        bulletCoolDown = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha5))
            
        if (bulletCoolDown)
        {
            bulletCoolDown = false;
            StartCoroutine("oneShoot");
        }
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
        yield return new WaitForSeconds(3f);
        bulletCoolDown = true;
        //} while (true) ;
    }
}
