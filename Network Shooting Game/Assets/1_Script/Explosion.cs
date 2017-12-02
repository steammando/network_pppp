using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionPower = 500f;
    public float explosionArea = 100f;
    public float explosionUplift = 200f;
    public GameObject effect;

    private Rigidbody2D bombRigidbody;

    void Awake()
    {
        bombRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void Update()
    {

        if (!bombRigidbody.isKinematic)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Baam();
                // add explosion particle
                Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    void Baam()
    {
        Collider2D[] aroundBombs = Physics2D.OverlapCircleAll(transform.position, explosionArea);

        foreach (Collider2D col in aroundBombs)
        {
            if (col.GetComponent<Rigidbody2D>() != null)
                Rigidbody2DExtension.AddExplosionForce(col.GetComponent<Rigidbody2D>(), explosionPower, transform.position, explosionArea, explosionUplift);
        }
    }


}
