using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    int prikey = 0;

    static VoteManager my = null;

    void Awake()
    {

        if (my == null)
        {
            my = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void SendVote()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 6)
        {
            StartCoroutine(VoteSend(prikey));
            prikey += 3;
        }
    }


    IEnumerator VoteSend(int p)
    {
        SocketCon.instance.sendToServer("VOTESET_" + p + "_1_10");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTENM_" + p + "_Select Balls!!!");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + p + "_1_Ball");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + p + "_2_Bomb");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + p + "_3_Meteor");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELED_" + p);
        yield return new WaitForSeconds(0.2f);

        SocketCon.instance.sendToServer("VOTESET_" + (p + 1) + "_1_10");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTENM_" + (p + 1) + "_Select Balls!!!");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 1) + "_1_Ball");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 1) + "_2_Bomb");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 1) + "_3_Meteor");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELED_" + (p + 1));
        yield return new WaitForSeconds(0.2f);

        SocketCon.instance.sendToServer("VOTESET_" + (p + 2) + "_1_10");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTENM_" + (p + 2) + "_Select Balls!!!");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 2) + "_1_Ball");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 2) + "_2_Bomb");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELST_" + (p + 2) + "_3_Meteor");
        yield return new WaitForSeconds(0.2f);
        SocketCon.instance.sendToServer("VOTELED_" + (p + 2));

        yield return null;
    }
}
