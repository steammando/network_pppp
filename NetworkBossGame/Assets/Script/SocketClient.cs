using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class SocketClient : MonoBehaviour
{

    //  SocketClient part is changed! SocketClient.cs -> SocketCon.cs...
    //  This source code is for testing purposes only.
    //  Refer to SocketCon for actual code.s
    //////////////////////////////////////////////////////////////////////
    public TcpClient clientSocket;
    public string ipAddress = "127.0.0.1";
    public int portNumber = 8000;
    public bool readReady;

    private Socket sock;
    private NetworkStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    private String data;
    private string con;
    // Use this for initialization

    void Start()
    {
        //sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);

        con = "connect";
        setupSocket();
        readReady = false;
    }

    // Update is called once per frame
    void Update()
    {

        readSocket();
    }
    public void setupSocket()
    {
        try
        {
            clientSocket = new TcpClient(ipAddress, portNumber);
            stream = clientSocket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            Debug.Log("Connection Success!");
        }
        catch (socketException e)
        {
            Debug.Log("Socket Error " + e);
            print("Connection failed");
        }
    }

    public void readSocket()
    {
        if (stream.DataAvailable)
            data = reader.ReadLine();
    }
    public void writeSocket(String message)
    {
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        stream.Flush();
        //clientSocket.
    }
    public class socketException : Exception
    {
        public socketException(string e)
            : base(e)
        { }
    }
}
