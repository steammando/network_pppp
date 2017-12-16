using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour {
    public GameObject clrUI;
    public int stageNumber;
    public GameObject[] goalObj;

    private bool clear;

	// Use this for initialization
	void Start () {
        clear = false;

        goalObj = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        if (!clear)
            if (CheckAllKill())
            {
                Debug.Log("ASD");
                clear = true;
                if (MapMgr.self.stageNum <= stageNumber)
                    MapMgr.self.stageNum = stageNumber + 1;
                clrUI.SetActive(true);
            }
	}

    bool CheckAllKill()
    {
        bool all = true;

        foreach(GameObject obj in goalObj)
        {
            if (obj != null) all = false;
        }
        return all;
    }
}
