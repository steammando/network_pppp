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
        //color.a = 0f;
        color = sr.color;
        color.a = 0f;
        sr.color = color;
        position = gameObject.transform.position;
        afterMathPos.y = -4;
    }
	void OnTriggerEnter2D(Collider2D _col)
    {
        position.x = GameManager.instance.transform.position.x;
        gameObject.transform.position = position;
        afterMathPos.x = position.x;
        if(_col.CompareTag("Player")&&sr.color.a!=0)
        {
            Debug.Log("Damaged!");
            GameManager.instance.player.Damaged(damage);
        }
    }
	// Update is called once per frame
	void Update () {
        
    }
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
        for(int i=0;i<4;i++)
        {
            temp=Instantiate(beamTracer, afterMathPos, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0, 0);
            temp = Instantiate(beamTracer, afterMathPos, Quaternion.identity);
            temp.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0, 0);
            yield return new WaitForSeconds(1f);
        }
        color.a = 0f;
        sr.color = color;
        anim.ResetTrigger("Shoot");
        anim.SetTrigger("Idle");
        yield return null;
    }
}
