using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInfo : MonoBehaviour {
    // Insert coin value
    int money = 5;

    private void Awake()
    {
        BoxCollider2D tempCol = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("돈이다!");
            col.gameObject.GetComponent<PlayerMove>().Earn_Money(money);
            SoundManager.soundManager.PlayCoinGetSound();
            gameObject.SetActive(false);
            Destroy(gameObject);
            //When contact the player, call the Earn_money function, and after make a coin acquisition sound, destroy this object.
        }
    }
}
