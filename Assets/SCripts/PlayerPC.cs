using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayerPC :NetworkBehaviour
{
    #region Variables
    VideoPlayerManager managerScript;
    public Material DefaultMat;
    PCXMLAndPlayCommand xmlScript;
    [SyncVar (hook="OnAddressChanged")]
    public string Address=" ";
    Text clientsText;
    public GameObject environmentObj;
  public List<GameObject> players; 
  Toggle muteHeadSets;
Toggle muteServer;
bool isServerMuted;
bool isHeadSetsMuted;
//-------
GameManagerIdes gmMngrIdsScrpt;
GameObject gameManger;
    #endregion
    public override void OnStartLocalPlayer()
    {
      gameManger=GameObject.FindGameObjectWithTag("GameManager");
      managerScript=gameManger.GetComponent<GameManagerRefrences>().mngr;
      environmentObj=gameManger.GetComponent<GameManagerRefrences>().Environment;
      gmMngrIdsScrpt=gameManger.GetComponent<GameManagerIdes>();
      xmlScript=gameManger.GetComponent<PCXMLAndPlayCommand>();
            if(isServer)
            {
              
              clientsText=gameManger
     .GetComponent<GameManagerRefrences>().clientsTxt.GetComponent<Text>();
             Address=xmlScript.OnVideoNameAsked();
             muteHeadSets=GameObject.Find("MuteHeadSets").GetComponent<Toggle>();
             if(PlayerPrefs.GetInt("isHeadSetsMuted",0)==1)
             {
                 muteHeadSets.isOn=true;
             }
             else
             {
               muteHeadSets.isOn=false;
             }
           	 muteHeadSets.onValueChanged.AddListener(delegate{OnMuteHeadSetsChanged();});
           	 muteServer=GameObject.Find("MuteServer").GetComponent<Toggle>();
              if(PlayerPrefs.GetInt("isServerMuted",0)==1)
             {
                 muteServer.isOn=true;
             }
             else
             {
               muteServer.isOn=false;
             }
          	 muteServer.onValueChanged.AddListener(delegate{OnMuteServerChanged();});
            } 
    }
    void Start()
    {   
         managerScript=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerRefrences>().mngr;
         DefaultMat=managerScript.DefaultMat;
         if(!isLocalPlayer)
         return;
    }
    void Update()
   {
          if(!isLocalPlayer)
       return;
       if(isServer)
       {
      //  RenderSettings.skybox=DefaultMat;
      //  //----------------------------Start The Video
      //    if(Input.GetKeyDown(KeyCode.Space))
      //      {
      //        RpcPlay();
      //        managerScript.OnSpacePressed();
      //        OnMuteHeadSetsChanged();
      //        OnMuteServerChanged();
      //      }
      //   //----------------------------Stop The Video
      //      if(Input.GetKeyDown(KeyCode.Escape))
      //      {
      //        RpcStop();
      //        managerScript.StopPlaying();
      //      }
         players.Clear();
         IEnumerable<GameObject> plyrs=GameObject.FindGameObjectsWithTag("Player");
        players.AddRange (plyrs);
       }
   }
   public void OnMuteHeadSetsChanged()
{
	if(!isServer)
	return;
    RpcOnMuteHeadSets(muteHeadSets.isOn);
    if(muteHeadSets.isOn)
    {
      PlayerPrefs.SetInt("isHeadSetsMuted",1);
    }
    else
    {
       PlayerPrefs.SetInt("isHeadSetsMuted",0);
    }
}
public void OnMuteServerChanged()
{
   if(!isServer)
   return;
   managerScript.plyr.SetDirectAudioMute(0,muteServer.isOn);
  if (!muteServer.isOn)
  {
	 managerScript.src.volume=1.0f;
     PlayerPrefs.SetInt("isServerMuted",0);
  }
  else
  {
    managerScript.src.volume=0;
    PlayerPrefs.SetInt("isServerMuted",1);
  }
}
   [ClientRpc]
   public void RpcPlay()
   { 
      managerScript.OnStartCommanded();
   } 
      public void OnAddressChanged(string value)
      {
          Address=value;
      }
    public void OnApplicationQuit()
    {
        if(!isServer)
        return;
        if(!isLocalPlayer)
        return;
       
     RpcStop();
     managerScript.StopPlaying();
    }
    public void Quit()
    {
      RpcStop();
    }
 [ClientRpc]
 public void RpcStop()
  { 
   managerScript.StopPlaying();
  }
  [ClientRpc]
public void RpcOnMuteHeadSets(bool mute)
{
 if(isServer)
 return;
managerScript.MuteChange(mute);
}
#region  set the address and ready
//Call when receiveing tcp order to set the Address
public void OnAddressSet(int address)
{
 CmdAddressSet(address);
}
[Command]
public void CmdAddressSet(int address)
{
AddressSet(address);
RpcAddressSet(address);
}
[ClientRpc]
public void RpcAddressSet(int address)
{
   if(isServer)
   return;
   AddressSet(address);
}
public void AddressSet(int address)
{
managerScript.AskForAddress(address.ToString());
}
#endregion
#region play the video
//call by tcp order
public void OnPlayOrdered(int[] whoToPlay)
{
    CmdPlayOrdered(whoToPlay);
}
public void PlayOrdered(int[] whoToPlay)
{
  if(gmMngrIdsScrpt.playTheVideoFlag==1)
  {
  managerScript.OnStartCommanded();
  }
}
[Command]
public void CmdPlayOrdered(int[] whoToPlay)
{
  PlayOrdered(whoToPlay);
  RpcPlayOrdered(whoToPlay);
}
[ClientRpc]
public void RpcPlayOrdered(int[] whoToPlay)
{
  if(isServer)
  return;
  PlayOrdered(whoToPlay);
}
#endregion
#region  StopTheVideo
//call by tcp order
public void OnStopOrdered()
{
  CmdStopOrdered();
}
public void StopOrdered()
{
  managerScript.StopPlaying();
}
[Command]
public void CmdStopOrdered()
{
  StopOrdered();
  RpcStopOrdered();

}
[ClientRpc]
public void RpcStopOrdered()
{
  if(isServer)
  return;
  StopOrdered();
}
#endregion
}
