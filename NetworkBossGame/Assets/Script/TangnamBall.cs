using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangnamBall : MonoBehaviour {

    public GameObject effect;
    private Rigidbody2D rb2d;
    private Vector3 moveVector;
    private int TTL;
    private float speed = 5f;
	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        //time to live!
        TTL = 2;
	}
    //set speed& moving direction...
    public void AddForce(int direction)
    {
        if(direction==1)
            rb2d.AddForce(new Vector3(Random.Range(100,200), 400, 0));
        else
            rb2d.AddForce(new Vector3(Random.Range(-200, -100), 400, 0));
    }
   
    void OnTriggerEnter2D(Collider2D _col)
    {
        //when collide with floor...
        if(_col.CompareTag("Floor")|| _col.CompareTag("MainFloor"))
        {
            //ttl decrease...
            TTL--;
            if (TTL == 0)
            {
                StartCoroutine("DestroyGameObject");
            }
            //moveVector is moving velocity
            moveVector = rb2d.velocity;
            moveVector.y = moveVector.y * -1;

            rb2d.velocity = moveVector;
        }
    }
    //when 3collision... -> destroy gameObject
    IEnumerator DestroyGameObject()
    {
        //game effect...
        GameObject effect_temp;

        effect_temp=Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);//transparency
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;//delayed destroy...

        yield return new WaitForSeconds(0.5f);

        Destroy(effect_temp);
        Destroy(gameObject);
    }
}
