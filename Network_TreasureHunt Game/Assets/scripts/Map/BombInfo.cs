using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInfo : MonoBehaviour {
    Vector3 thisBlockPos;

    float damage = 3;
    public static BombInfo instance;
    private void Awake()
    {
        instance = this;
        thisBlockPos = transform.position;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.soundManager.PlayExplosionSound();
            col.gameObject.GetComponent<PlayerMove>().Have_Damage(damage);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
