﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class SocketCon : MonoBehaviour
{
    public BallSetting ballsetting;

    //private static Socket m_Socke;
    public static SocketCon instance;

    static Socket m_Socket;
    public string iPAdress = "192.168.170.24";
    public const int kPort = 8000;

    private int SenddataLength;
    private int ReceivedataLength;

    private bool socketActive;
    private Thread thread = null;
    private byte[] Sendbyte;
    //receive buffer.
    private byte[] Receivebyte = new byte[2000];
    private string ReceiveString;

    // Use this for initialization
    void Start()
    {
        socketActive = true;
        instance = this;
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.170.24"), 8000);

        try
        {
            m_Socket.Connect(localEndPoint);
        }
        catch
        {
            Console.Write("Unable to connect to remote end point!\r\n");
            
        }
        
        StringBuilder sb = new StringBuilder(); // String Builder Create

        sb.Append("Connection");

        try
        {
            //first data sending...
            SenddataLength = Encoding.Default.GetByteCount(sb.ToString());
            Sendbyte = Encoding.Default.GetBytes(sb.ToString());
            //m_Socket.Send(Sendbyte, Sendbyte.Length, 0);
        }
        catch (SocketException err)
        {
            //socket error occure''
            Debug.Log("Socket send or receive error! : " + err.ToString());
        }

        thread = null;
        if(thread==null)
        {
            Debug.Log("Thread Run!");
            thread = new Thread(RunThread);
            thread.Start();
        }
        //StartCoroutine("SocketRead_Pattern");*/
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
        if (socketActive)
        {
            sb.Append(message);
            try
            {
                SenddataLength = Encoding.Default.GetByteCount(sb.ToString());
                Sendbyte = Encoding.Default.GetBytes(sb.ToString());

                m_Socket.Send(Sendbyte, Sendbyte.Length, 0);
            }
            catch (SocketException e)
            {
                Debug.Log("SocketSendError " + e);
            }
        }
    }

    /* RunThread(): using thread, This function accepts data from the server.
     * @input: none_
     * @output: none_
     **************************************************************************/
    void RunThread()
    {
        Debug.Log("Thread is running...");

        int n = 0;

        while(true)
        {
            Array.Clear(Receivebyte, 0, Receivebyte.Length);
            try
            {
                m_Socket.Receive(Receivebyte);
                ReceiveString = Encoding.Default.GetString(Receivebyte);

                string []temp = ReceiveString.Split('_');
                ReceivedataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());
                

                Debug.Log("Now... -> " + temp[0]);
                if (String.Equals(temp[0], "VOTEEND"))
                {
                    if(int.Parse(temp[2]) == 1)
                    {
                        Debug.Log(temp[2]);
                        ballsetting.AddBall(1);
                    }
                    else if(int.Parse(temp[2]) == 2)
                    {
                        Debug.Log(temp[2]);
                        ballsetting.AddBall(2);
                    }
                    else if (int.Parse(temp[2]) == 3)
                    {
                        Debug.Log(temp[2]);
                        ballsetting.AddBall(3);
                    }
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
    public void endSocketCon()
    {
        socketActive = false;
        m_Socket.Disconnect(false);
        m_Socket.Close();
    }
}