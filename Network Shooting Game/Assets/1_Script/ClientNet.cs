using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class ClientNet : MonoBehaviour {
    public TcpClient clientSocket;
    public string ipAddress = "127.0.0.1";
    public int portNumber = 8000;
    public bool readReady;

    private NetworkStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    private String data;
    private string con;
    // Use this for initialization
    void Start()
    {
        con = "connect";
        setupSocket();
        readReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stream.DataAvailable)
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
        data = reader.ReadLine();
    }
    public class socketException : Exception
    {
        public socketException(string e)
            : base(e)
        { }
    }

}
