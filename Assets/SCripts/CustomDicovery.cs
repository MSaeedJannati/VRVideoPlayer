using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CustomDicovery : NetworkDiscovery
{
 public string adrs=string.Empty;
 NetworkManager manager;
int crTime;
 Text TimerTxt;
    void Start()
     {
        crTime=(int)Time.time;
        TimerTxt=GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
       manager=GetComponent<NetworkManager>();
        Initialize();
        if(Application.platform==RuntimePlatform.WindowsEditor || Application.platform==RuntimePlatform.WindowsPlayer)
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
           adrs=fromAddress;
           if(manager.client==null)
           Connect(fromAddress);
     }
    public void Connect(string Address)
     {
          string[] j=Address.Split(':');
          manager.networkAddress=j[3];
          manager.StartClient();
     }
    public void FixedUpdate()
     { 
         TimerTxt.text=crTime.ToString();
          StartBroadcasting();
          StartListening();
          
     }
        
    public void StartBroadcasting()
     {
        if(Application.platform!=RuntimePlatform.WindowsEditor && Application.platform!=RuntimePlatform.WindowsPlayer)
        return;
        if(running)
        return;
        manager.StopHost();
        StopBroadcast();
        Initialize();
        StartAsServer();
        manager.StartHost();
     }
    public void StartListening()
     {
        if(Application.platform==RuntimePlatform.WindowsEditor || Application.platform==RuntimePlatform.WindowsPlayer)
        return;
        if(running)
        return;
        if(manager.client!=null)
        manager.client.Disconnect();
        StopBroadcast();
        Initialize();
        StartAsClient();    
     } 
}
