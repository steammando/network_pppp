using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
namespace Clicker
{
    public class NetworkConsole : MonoBehaviour
    {

        public static NetworkConsole instance;



        static Socket my_socket;


        public string iPaddress = "192.168.170.24";
        public const int Port = 8000;

        private int SendDataLength;
        private int ReceiveDataLength;

        private Thread thread = null;
        private byte[] SendByte;
        private byte[] ReceiveByte = new byte[2000];
        private string ReceiveString;


        // Use this for initialization
        void Start()
        {
            instance = this;
            startVote();
        }

        private void OnApplicationQuit()
        {
            my_socket.Close();
            my_socket = null;
        }
        public string receiveFromServer()
        {
            try
            {
                my_socket.Receive(ReceiveByte);
                ReceiveString = Encoding.Default.GetString(ReceiveByte);
                ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());

            }
            catch (SocketException e)
            {
                Debug.Log("SocketError " + e);
            }
            return ReceiveString;
        }
        public void sendToServer(String message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(message);
            try
            {
                SendDataLength = Encoding.Default.GetByteCount(sb.ToString());
                SendByte = Encoding.Default.GetBytes(sb.ToString());

                my_socket.Send(SendByte, SendByte.Length, 0);
            }
            catch (SocketException e)
            {
                Debug.LogError("SocketSendError " + e);
            }
        }

        public void startVote()
        {
            my_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.170.24"), 8000);

            try
            {
                my_socket.Connect(localEndPoint);
                Debug.Log("Connect!");
            }
            catch
            {
                Console.Write("Unable to connect to remote end point!\r\n");
            }
            
            StringBuilder sb = new StringBuilder(); // String builder Create
            sb.Append("Connection");

            try
            {
                SendDataLength = Encoding.Default.GetByteCount(sb.ToString());
                SendByte = Encoding.Default.GetBytes(sb.ToString());
            }
            catch (SocketException err)
            {
                Debug.Log("Socket send or receive error!  : " + err.ToString());
            }
            thread = null;
            if (thread == null)
            {
                Debug.Log("Thread Run!");
                thread = new Thread(RunThread);
                thread.Start();
            }
            StartCoroutine("SocketRead_Keyword");
        }

        IEnumerator SocketRead_Keyword()
        {
            sendToServer("VOTESET_300_2_50");
            yield return new WaitForSeconds(0.1f);
            sendToServer("VOTENM_300_wallVote"); //투표 생성
            yield return new WaitForSeconds(0.1f);

            sendToServer("VOTEKEY_300_wall"); //투표 키워드 설정
            yield return new WaitForSeconds(0.1f);

            sendToServer("VOTEKS_300"); //키워드 투표 시작
            yield return new WaitForSeconds(0.1f);

        }
        void RunThread()
        {
            Debug.Log("Thread Run_ nou");
            int n = 0;
            while (true)
            {
                Array.Clear(ReceiveByte, 0, ReceiveByte.Length);
                try
                {
                    my_socket.Receive(ReceiveByte);
                    ReceiveString = Encoding.Default.GetString(ReceiveByte);
                    string[] temp = ReceiveString.Split('_');
                    ReceiveDataLength = Encoding.Default.GetByteCount(ReceiveString.ToString());

                    Debug.Log("Now... -> " + temp[0]);
                    if (String.Equals(temp[0], "VOTEBAK"))
                    {
                        VoteManager.instance.VoteRST(temp);
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
            if (my_socket != null)
                my_socket.Close();
        }
    }
}