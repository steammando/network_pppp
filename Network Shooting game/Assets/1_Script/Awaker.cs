using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awaker : MonoBehaviour {
    public VoteManager vote;
    public BallSetting ballsets;

    void Start()
    {
        vote = GameObject.Find("NetworkMGR").GetComponent<VoteManager>();
        vote.SendVote();

        ballsets = GameObject.Find("BallManager").GetComponent<BallSetting>();
        ballsets.ballSet.Clear();
    }
}
