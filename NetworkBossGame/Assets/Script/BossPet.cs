using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPet : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector3 playerPostition;
    private GameObject bullet;
    private int direction;
    private int times;
	// Use this for initialization
	void Awake () {
        playerPostition = GameManager.instance.player.transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        times = 0;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void run(int direction)
    {
        gameObject.transform.localScale = new Vector3(direction*0.5f, 1*0.5f, 1*0.5f);
        if (direction == 1)
        {
            
            rb2d.velocity = new Vector3(8f, 0, 0);
        }
        else
        {
            rb2d.velocity = new Vector3(-8f, 0, 0);
        }
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("UserBullet"))
        {
            StartCoroutine("DeadPet");
            Destroy(_col);
                }
        if (_col.CompareTag("Wall"))
        {
            times++;
            if (times == 2)
            {
                StartCoroutine("DeadPet");
                GameManager.instance.boss.patternBoolean[4] = true;
            }
        }
    }
    IEnumerator DeadPet()
    {
        Color tempCol;
        for (int i = 0; i < 5; i++)
        {
            tempCol=gameObject.GetComponent<SpriteRenderer>().color;
            tempCol.g = 2 * i;
            gameObject.GetComponent<SpriteRenderer>().color = tempCol;
            yield return new WaitForSeconds(0.1f);
        }
        
        Destroy(gameObject);
        yield return null;
    }
}
