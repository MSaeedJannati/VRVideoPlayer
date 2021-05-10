using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class XmlBasedDiscovery : NetworkDiscovery
{
 public string adrs=string.Empty;
 NetworkManager manager;
 bool hadConnectionProblem=false;
 NetworkManagerFindIfServer findServerScript;
 GameManagerTImer timerScript;
 bool IsServer=false;
 int broadcastTime=0;
 string hstAddrss=string.Empty;
 int deltaTime;
    void Start()
     {
        findServerScript=GetComponent<NetworkManagerFindIfServer>();
        IsServer=findServerScript.IsServer();
       manager=GetComponent<NetworkManager>();
       timerScript=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerTImer>();
        Initialize();
        if(IsServer)
         { 
           StartAsServer();
           manager.StartHost();
         }
        else
         {
            StartAsClient(); 
         }
     }
    public override void OnReceivedBroadcast(string fromAddress, string data)
     {
          broadcastTime=(int)Time.time;
           adrs=fromAddress;
           if(manager.client==null || hadConnectionProblem)
           {
                 if(manager.client!=null)
                 manager.client.Disconnect();    
           Connect(fromAddress);
           }
     }
    public void Connect(string Address)
     {
          string[] j=Address.Split(':');
          manager.networkAddress=j[3];
          manager.StartClient();
          hadConnectionProblem=false;
          broadcastTime=(int)Time.time;
     }
    public void FixedUpdate()
     {    StartBroadcasting();
        StartListening();
         deltaTime=(int)Time.time-broadcastTime;
       if(!IsServer)
      {
       timerScript.SetText(deltaTime);
      if(deltaTime>5)
      hadConnectionProblem=true;
      }
     }  
    public void StartBroadcasting()
     {
        if(!IsServer)
        return;
        if(running)
        return;
        if(deltaTime<5)
        return;
        broadcastTime=(int)Time.time;
        if(manager.client!=null)
        manager.StopHost();
        StopBroadcast();
        Initialize();
        StartAsServer();
        manager.StartHost();
     }
    public void StartListening()
     {
        if(IsServer)
        return;
        if(running)
        return;
        if(manager.client!=null)
        manager.client.Disconnect();
        StopBroadcast();
        Initialize();
        StartAsClient();
     } 
     public void StartListeningAfterDelay()
     {
         manager.client.Disconnect();
        StopBroadcast();
        Initialize();
        StartAsClient(); 
     }
}
