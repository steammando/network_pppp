using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BallSetting : MonoBehaviour
{
    public Queue<GameObject> ballSet = new Queue<GameObject>();
    public GameObject[] Item;
    //public Transform catapult;

    private GameObject ball;
    private float setTime = 3.0f;
    private float timeSpan = 2.5f;
    private bool loadingBall = false;

    private int curNum = 0;

    static BallSetting my = null;

    void Awake()
    {
        if (my == null)
        {
            my = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        //if queue has ball
        if (ballSet.Count > 0 && (ball == null || ball.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic))
        {
            ball = ballSet.Dequeue();//get ball in queue
            loadingBall = true;
        }
        if (loadingBall)
        {
            timeSpan += Time.deltaTime;
            //wating time
            if (timeSpan >= setTime)
            {
                //generate ball to catapult
                ball = Instantiate(ball, new Vector3(-31.79f, -8.76f, 0), Quaternion.identity);
                timeSpan = 0.0f;//time reset
                loadingBall = false;
            }
        }
    }

    //add ball in queue
    public void AddBall(int num)
    {
        ballSet.Enqueue(Item[num - 1]);
    }

}
