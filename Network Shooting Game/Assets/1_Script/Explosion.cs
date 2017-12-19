using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionPower = 500f;
    public float explosionArea = 100f;
    public float explosionUplift = 200f;
    public GameObject effect;

    private AudioClip expS;
    private Rigidbody2D bombRigidbody;

    void Awake()
    {
        bombRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if ball is launched
        if (!bombRigidbody.isKinematic)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Baam();
                //explosion particle generate
                Instantiate(effect, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                //Destroy(this.gameObject);
                StartCoroutine(DestoyThis(2.0f));
            }
        }
    }

    //add explosion force other
    void Baam()
    {
        Collider2D[] aroundBombs = Physics2D.OverlapCircleAll(transform.position, explosionArea);

        foreach (Collider2D col in aroundBombs)
        {
            if (col.GetComponent<Rigidbody2D>() != null)
                Rigidbody2DExtension.AddExplosionForce(col.GetComponent<Rigidbody2D>(), explosionPower, transform.position, explosionArea, explosionUplift);
        }
    }

    IEnumerator DestoyThis(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
