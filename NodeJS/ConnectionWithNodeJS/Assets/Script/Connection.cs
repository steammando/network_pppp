using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using UnityEngine;

public class Connection : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Connect("127.0.0.1", "Good Morning!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    static void Connect(String server,String message)
    {
        try
        {
            int port = 3000;
            TcpClient client = new TcpClient(server, port);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent:{0} " + message);

            data = new Byte[256];
            String responseData = String.Empty;

            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0} " + responseData);
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Debug.Log("ArgumentNullException " + e);
        }
        catch(SocketException e)
        {
            Debug.Log("SocketExcpetion " + e);
        }
    }
}
