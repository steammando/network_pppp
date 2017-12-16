using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thone : MonoBehaviour {
    public GameObject thone;

    private bool atk;
    private Color tempColor;
    private SpriteRenderer sr;
    private Vector3 positonVector;
    private Vector3 thonePosition;
    private GameObject thoneObject;
	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        tempColor = sr.color;
        tempColor.a = 0f;
        sr.color = tempColor;
        atk = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (!atk)
        {
            positonVector = gameObject.transform.position;
            positonVector.x = GameManager.instance.player.transform.position.x;
            gameObject.transform.position = positonVector;
        }
    }
    public void stab()
    {
        GameManager.instance.boss.patternBoolean[3] = false;
        tempColor.a = 255f;
        atk = true;
        sr.color = tempColor;
        thonePosition = gameObject.transform.position;
        thonePosition.y -= 3;
        thoneObject=Instantiate(thone, thonePosition, Quaternion.identity);
        StartCoroutine("ThoneUp");
    }
    IEnumerator ThoneUp()
    {
        yield return new WaitForSeconds(1f);
        for(int i=0;i<15;i++)
        {
            thonePosition = thoneObject.gameObject.transform.position;
            thonePosition.y += 8 / 30f;
            thoneObject.gameObject.transform.position = thonePosition;
            yield return new WaitForSeconds(0.03f);
        }
        for (int i = 0; i < 5; i++)
        {
            thonePosition = thoneObject.gameObject.transform.position;
            thonePosition.y -= 8 / 10f;
            thoneObject.gameObject.transform.position = thonePosition;
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(thoneObject);
        //GameManager.instance.boss.patternBoolean[3] = true;
        atk = false;
        tempColor.a = 0f;
        sr.color = tempColor;
        yield return null;
    }
}
