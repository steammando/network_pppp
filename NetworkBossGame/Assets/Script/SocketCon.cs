using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketCon : MonoBehaviour
{
    private Socket m_Socket;

    public string iPAdress = "127.0.0.1";
    public const int kPort = 8000;

    private int SenddataLength;
    private int ReceivedataLength;

    private Boss boss;
    private Thread thread = null;
    private byte[] Sendbyte;
    private byte[] Receivebyte = new byte[2000];
    private string ReceiveString;

    // Use this for initialization
    void Start()
    {
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 100000);
        m_Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100000);
        boss = GameManager.instance.boss;
        try
        {
            
            IPAddress ipAddr = System.Net.IPAddress.Parse(iPAdress);
            IPEndPoint ipEndPoint = new System.Net.IPEndPoint(ipAddr, kPort);
            m_Socket.Connect(ipEndPoint);
            Debug.Log("Connection");
        }
        catch (SocketException SCE)
        {
            Debug.Log("Socket connect error! : " + SCE.ToString());
            return;
        }

        StringBuilder sb = new StringBuilder(); // String Builder Create
        sb.Append("Connection");
        try
        {
            SenddataLength = Encoding.Default.GetByteCount(sb.ToString());
            Sendbyte = Encoding.Default.GetBytes(sb.ToString());
            m_Socket.Send(Sendbyte, Sendbyte.Length, 0);

            /*
            m_Socket.Receive(Receivebyte);
            ReceiveString = Encoding.Default.GetString(Receivebyte);
            ReceivedataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());
            Debug.Log("Receive Data : " + ReceiveString + "(" + ReceivedataLength + ")");
            */
        }
        catch (SocketException err)
        {
            Debug.Log("Socket send or receive error! : " + err.ToString());
        }
        thread = null;
        if(thread==null)
        {
            Debug.Log("Thread Run!");
            thread = new Thread(RunThread);
            thread.Start();
        }
        StartCoroutine("SocketRead_Pattern");
    }
    
    void Update()
    {

    }
    void OnApplicationQuit()
    {
        m_Socket.Close();
        m_Socket = null;
    }
    public string receiveFromServer()
    {
        try {
            m_Socket.Receive(Receivebyte);
            ReceiveString = Encoding.Default.GetString(Receivebyte);
            ReceivedataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());
            //Debug.Log("Receive Data : " + ReceiveString + "(" + ReceivedataLength + ")");
        }
        catch(SocketException e)
        {
            Debug.Log("SocketError " + e);
        }
        return ReceiveString;
    }

    public void sendToServer(string message)
    {
        StringBuilder sb = new StringBuilder();
        
        sb.Append(message);
        try {
            SenddataLength = Encoding.Default.GetByteCount(sb.ToString());
            Sendbyte = Encoding.Default.GetBytes(sb.ToString());

            m_Socket.Send(Sendbyte, Sendbyte.Length, 0);
        }
        catch(SocketException e)
        {
            Debug.Log("SocketSendError "+e);
        }
    }

    IEnumerator SocketRead_Pattern()
    {
        while (true)
        {
            sendToServer("VOTENM_100");
            yield return new WaitForSeconds(0.1f);
            sendToServer("VOTELST_100_1_RAGE");
            yield return new WaitForSeconds(0.1f);
            sendToServer("VOTELST_100_2_RAZER");
            yield return new WaitForSeconds(0.1f);
            sendToServer("VOTELST_100_3_PUNCH");
            yield return new WaitForSeconds(0.1f);
            sendToServer("VOTELED_100");

            yield return new WaitForSeconds(11f);
        }
    }

    void RunThread()
    {
        Debug.Log("Thread Run_ nou");
        int n = 0;
        while(true)
        {
            Array.Clear(Receivebyte, 0, Receivebyte.Length);
            try
            {
                //Debug.Log("is Null? " + ReceiveString);
                m_Socket.Receive(Receivebyte);
                ReceiveString = Encoding.Default.GetString(Receivebyte);
                string []temp = ReceiveString.Split('_');
                ReceivedataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());

                if (String.Equals(temp[0], "Vote"))
                {
                    Debug.Log(temp[0]);
                    boss.PatternValid(n);
                    n++;
                    if (n >= 3)
                        n = 0;
                }
                temp = null;
            }
            catch (SocketException e)
            {
                Debug.Log("SocketError " + e);
                thread.Abort();
            }

        }
    }
}
