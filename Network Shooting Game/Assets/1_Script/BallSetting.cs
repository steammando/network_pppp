using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSetting : MonoBehaviour {
    public Queue<GameObject> ballSet = new Queue<GameObject>();
    public GameObject[] Item;
    public Transform catapult;

    private GameObject ball;
    private float setTime = 3.0f;
    private float timeSpan = 2.5f;
    private bool loadingBall = false;

    void Awake()
    {
        ballSet.Enqueue(Item[2]);
        ballSet.Enqueue(Item[1]);
    }

    void Start () {

	}
	
	void Update () {
        if (ballSet.Count > 0 && (ball == null || ball.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic))
        {
            ball = ballSet.Dequeue();
            loadingBall = true;
        }
        if (loadingBall)
        {
            timeSpan += Time.deltaTime;

            if(timeSpan >= setTime)
            {
                ball = Instantiate(ball, catapult.transform.position, Quaternion.identity);
                timeSpan = 0.0f;
                loadingBall = false;
            }
        }
	}

    void AddBall(GameObject newBall)
    {
        ballSet.Enqueue(newBall);
    }
    
}
