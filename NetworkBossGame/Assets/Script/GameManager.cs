using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//game manager class is 
//The basis of the overall game
public class GameManager : MonoBehaviour {

    private int GameDifficult;
    public Boss boss;
    public Player player;
    public Beam beam;
    public GameObject text;
    public int stage;
    public bool socket_boolean;
    private bool gameOver;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        gameOver = false;
        socket_boolean = false;
        //DontDestroyOnLoad(gameObject);

        GameDifficult = 1;
        stage = 1;

        boss = GameObject.FindObjectOfType<Boss>();
        text = GameObject.Find("UI_Canvas");
        player = GameObject.FindObjectOfType<Player>();
        beam = GameObject.FindObjectOfType<Beam>();
    }

    //Initializes the game for each level.
    //current state --> not used
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
        SocketCon.instance.endSocketCon();
        text.transform.FindChild("Canvas").gameObject.SetActive(true);
        gameOver = true;
    }
    //load gameover scene...
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
