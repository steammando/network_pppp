using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Test");
    }
    public void Re()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
