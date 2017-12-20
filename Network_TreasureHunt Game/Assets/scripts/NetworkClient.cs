using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;

public class NetworkClient : MonoBehaviour {

    public TcpClient clientSocket;
    public string iPaddress = "127.0.0.1";
    public int portNumber = 8000;
    public bool readReady;

    private Socket sock;
    private NetworkStream stream;
    private StreamReader reader;
    private StreamWriter writer;

    private String data;
    private string con;


	// Use this for initialization
	void Start () {
        con = "connect";
        setupSocket();
        readReady = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        readSocket();
	}

    public void setupSocket()
    {
        try
        {
            clientSocket = new TcpClient(iPaddress, portNumber);
            stream = clientSocket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            Debug.Log("Connection Success!");
        }
        catch(SocketException e)
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
    }
    public class socketException : Exception {
        public socketException(string e)
            : base(e)
        { }
    }


}
