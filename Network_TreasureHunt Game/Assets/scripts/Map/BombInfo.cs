using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInfo : MonoBehaviour {
    Vector3 thisBlockPos;

    float damage = 2;
    public static BombInfo instance;
    private void Awake()
    {
        instance = this;
        thisBlockPos = transform.position;
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //Debug.LogError("폭탄 받아라!!!");
            SoundManager.soundManager.PlayExplosionSound();
            col.gameObject.GetComponent<PlayerMove>().Have_Damage(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
