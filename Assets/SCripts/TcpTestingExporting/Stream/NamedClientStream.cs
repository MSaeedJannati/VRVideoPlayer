using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class NamedClientStream : MonoBehaviour {

    public string server;
    public int port;
    TcpClient client;
    NetworkStream streamSR;
    public GameObject Hostplayer;
    byte[] headSets;
    byte[] appNum;
    //--------------------------------------UI Variables
    // [SerializeField]
    // Text statusTxt;
    //--------------------------------------
    PlayerConnectionEvents serverCnnctnEvntsScrpt;
    
    // public CanvasListOfClients cnvslstofClntsScrpt;
    PlayerConnectionEvents cnnctmEvntsScrpt;
    void Start()
    {
        headSets=new byte[6];
        appNum=new byte[1];
        Connect();
    }
    void Update()
    {
        ReceiveMessegase();
    }
    public void ReceiveMessegase()
    {
        if(Hostplayer==null)
        {  
        Hostplayer=GameObject.Find("0");
        if(Hostplayer!=null)
        cnnctmEvntsScrpt=Hostplayer.GetComponent<PlayerConnectionEvents>();
        }
       if(client.Available>0)
       {
           streamSR = client.GetStream();
           byte[] firstByte=new byte[1];
           streamSR.Read(firstByte,0,1);
           switch(firstByte[0])
           {
               case 1:
               streamSR.Read(headSets,1,6);

               streamSR.Read(appNum,7,1);
               cnnctmEvntsScrpt.OnHandleHeadSets(headSets,appNum);
               break;
               case 2:
               cnnctmEvntsScrpt.OnStartApp();
               break;
               case 3:
               cnnctmEvntsScrpt.OnStopApp();
               break;
           }
       }    
    }
    public void Receive()
    {
        if (client.Available > 0)
        {
            Debug.Log("avaiulable");
            streamSR = client.GetStream();
            byte[] Score = new byte[client.Available];
            streamSR.Read(Score, 0, client.Available);
        }
    }
    public void BTN_con()
    {
        Connect();
        Debug.Log("Connected.");
    }
    public void BTN_Dis()
    {
            client.Close();
            Debug.Log("DisconnectedFromServer.");
    }
    public void BTN_SND(InputField fld)
    {
        if (client.Connected)
        {
            Debug.Log("Sending");
            string message = "Salam";
            if (fld.text != null)
            {
                message = fld.text;
            }
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
    public void Send(string dataString)
    {
        if (client.Connected)
        {
            string message = dataString;
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
    public void SendBytes(Byte[] DataInbytes)
    {
        if (client.Connected)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(DataInbytes, 0, DataInbytes.Length);
        }
    }
    void Connect()
    {
        client = new TcpClient("127.0.0.1", 1000);
    }
}