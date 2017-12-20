using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int GameDifficult;
    public Boss boss;
    public Player player;
    public Beam beam;
    public GameObject text;
    public int stage;
    private bool gameOver;
    // Use this for initialization
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        gameOver = false;
        //DontDestroyOnLoad(gameObject);
        GameDifficult = 1;
        stage = 1;
        boss = GameObject.FindObjectOfType<Boss>();
        text = GameObject.Find("UI_Canvas");
        player = GameObject.FindObjectOfType<Player>();
        beam = GameObject.FindObjectOfType<Beam>();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        Debug.Log("InItGame");
        if (stage == 1)
        {
            boss = GameObject.FindObjectOfType<Boss>();
            player = GameObject.FindObjectOfType<Player>();
            beam = GameObject.FindObjectOfType<Beam>();
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SocketCon.instance.endSocketCon();
	}
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.CompareTag("Floor") || _col.CompareTag("MainFloor"))
            Destroy(gameObject);
    }
    public void GameOver()
    {
<<<<<<< HEAD
        text.transform.Find("Canvas").gameObject.SetActive(true);
=======
        SocketCon.instance.endSocketCon();
        text.transform.FindChild("Canvas").gameObject.SetActive(true);
>>>>>>> 17e63e02cb910128f01266583002b3bcf81bf22f
        gameOver = true;
        
    }
    public void loadNextScene()
    {
        if (gameOver)
        {
            SocketCon.instance.endSocketCon();
            SceneManager.LoadScene("GameOver");
        }
    }
    public int getDifficult()
    {
        return GameDifficult;
    }
}
