using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    // Use this for initialization
    public GameObject beamTracer;
    private GameObject temp;
    private Vector3 position;
    private Vector3 afterMathPos;
    private SpriteRenderer sr;
    private Animator anim;
    private Color color;
    private int damage=20;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        color = sr.color;
        color.a = 0f;
        sr.color = color;
        position = gameObject.transform.position;
        afterMathPos.y = -4;
    }
	void OnTriggerEnter2D(Collider2D _col)
    {
        //ver0.2...-->current not used.
        /*
        position.x = GameManager.instance.transform.position.x;
        gameObject.transform.position = position;
        afterMathPos.x = position.x;*/
        
        if(_col.CompareTag("Player")&&sr.color.a!=0)
        {
            GameManager.instance.player.Damaged(damage);
        }
    }
    //it call shoot... --> I shoot a bullet in a radial direction.
    public void ShootBeam()
    {
        anim.SetTrigger("Beam");
        anim.SetTrigger("Shoot");
        color.a = 255f;
        sr.color = color;
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        //create 4 additional block
        for(int i=0;i<4;i++)
        {
            //right
            temp=Instantiate(beamTracer, afterMathPos, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0, 0);
            //eft
            temp = Instantiate(beamTracer, afterMathPos, Quaternion.identity);
            temp.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0, 0);
            yield return new WaitForSeconds(1f);
        }

        //if pattern is end, it will be transparent
        color.a = 0f;
        sr.color = color;
        //to idle state...
        anim.ResetTrigger("Shoot");
        anim.SetTrigger("Idle");
        //GameManager.instance.boss.patternBoolean[1] = true;
        GameManager.instance.boss.GetComponent<Animator>().SetTrigger("Idle");
        yield return null;
    }
}
