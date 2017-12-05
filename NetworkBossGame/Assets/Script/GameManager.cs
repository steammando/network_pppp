using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int GameDifficult;
    public Boss boss;
    public Player player;
    public Beam beam;
    public int stage;
    // Use this for initialization
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        GameDifficult = 1;
        stage = 1;
        boss = GameObject.FindObjectOfType<Boss>();
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
		
	}
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
    public void loadNextScene()
    {
        
    }
    public int getDifficult()
    {
        return GameDifficult;
    }
}
