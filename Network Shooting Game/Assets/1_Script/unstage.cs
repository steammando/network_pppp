using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class unstage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MapMgr.self.stageNum = 2;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Map");
    }
}
