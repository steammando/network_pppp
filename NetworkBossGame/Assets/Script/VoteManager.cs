using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public static VoteManager instance;
    private Boss boss;

    private bool bossExistance;
    private bool activeVote;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    //start() is ran on after awake...
    void Start()
    {
        if (GameManager.instance.boss != null)
        {
            boss = GameManager.instance.boss;
            bossExistance = true;
        }
        else bossExistance = false;
    }

    //VOTESET_primarykey_votetype_time
    //make a message(set vote for _time, this type is x)
    public string startVote(int primaryKey, int type, int _time)
    {
        Debug.Log("is error...?");
        String rst = "VOTESET_" + primaryKey.ToString() + "_" + type.ToString() + "_" + _time.ToString();
        Debug.Log("Str is "+rst);
        return rst;
    }

    //VOTENM_primary_text
    //set the vote's name,,,
    public string setVoteName(int primaryKey, string name)
    {
        String rst = "VOTENM_" + primaryKey.ToString() + "_" + name.ToString();
        return rst;
    }

    //VOTELST_primary_1~n_text
    //when n is -1, it is an end of entry...
    public string VoteEntry(int primaryKey, int n, string content)
    {
        String rst;
        rst = "VOTELST_" + primaryKey.ToString() + "_" + n.ToString() + "_" + content.ToString();
        return rst;
    }
    //when n or content not exist, it is end of entry
    public string VoteEntry(int primaryKey)
    {
        String rst;
        rst = "VOTELED_" + primaryKey.ToString();
        //VOTELED_primary
        return rst;
    }

    //VOTEEND_primary_result(number)
    public void VoteRST(String[] values)
    {
            if(String.Equals(values[1], "100"))
            {
                boss.PatternValid(int.Parse(values[2]));
            }
    }
}
