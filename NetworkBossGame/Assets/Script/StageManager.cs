using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

    //scene load
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Test");
    }
    public void Re()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
