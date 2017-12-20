using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public static VoteManager instance;
    private NetworkMapCreate NMC;
    private PlayerMove PM;

    private bool bossExistance;
    private bool activeVote;

    // Use this for initialization
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    //VOTESET_primarykey_votetype_time
    public string startVote(int primaryKey, int type, int _time)
    {
        String rst = "VOTESET_" + primaryKey.ToString() + "_" + type.ToString() + "_" + _time.ToString();
        return rst;
    }

    //VOTENM_primary_text
    public string setVoteName(int primaryKey, string name)
    {
        String rst = "VOTENM_" + primaryKey.ToString() + "_" + name.ToString();
        return rst;
    }



    //VOTEKEY_primary_keyword
    public string VoteKeywordSet(int primaryKey, string keyword)
    {
        String rst;
        rst = "VOTEKEY_" + primaryKey.ToString() + "_" + keyword.ToString();
        return rst;
    }


    //start Vote
    public string VoteKeyword(int primaryKey)
    {
        String rst;
        rst = "VOTEKS_" + primaryKey.ToString();
        //VOTEKS_primary
        return rst;
    }

    //VOTEEND_primary_result(number)
    public void VoteRST(String[] values)
    {
        if (int.Parse(values[1])==200)//(String.Equals(values[1], "200"))
        {
            Debug.Log("폭탄 좀 만드세요 제발");
            NetworkMapCreate.instance.vote_insert();
        }
        else
            Debug.Log("만들고 싶어도 투표 내용을 확인을 못해요:"+values[1]+"|");
    }
}