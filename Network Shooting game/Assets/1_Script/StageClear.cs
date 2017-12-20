using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour {
    public GameObject clrUI;
    public int stageNumber;
    public GameObject[] goalObj;

    private bool clear;

    //set clear object
	void Start () {
        clear = false;

        goalObj = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	void Update () {
        if (!clear)
            if (CheckAllKill())//is cleard
            {
                Debug.Log("ASD");
                clear = true;
                if (MapMgr.self.stageNum <= stageNumber)
                    MapMgr.self.stageNum = stageNumber + 1;
                clrUI.SetActive(true);
            }
	}

    //check all object killed
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
