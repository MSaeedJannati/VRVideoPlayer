using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerVolumeCnrtl : NetworkBehaviour {

#region Variables
Toggle muteHeadSets;
Toggle muteServer;
VideoPlayerManager managerScript;
#endregion
#region Properties
#endregion
#region Functions
public void Start()
{
	managerScript=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerRefrences>().mngr;
	if(!isLocalPlayer)
	return;
	if(!isServer)
	return;
	muteHeadSets=GameObject.Find("MuteHeadSets").GetComponent<Toggle>();
	muteHeadSets.onValueChanged.AddListener(delegate{OnMuteHeadSetsChanged();});
	muteServer=GameObject.Find("MuteServer").GetComponent<Toggle>();
	muteServer.onValueChanged.AddListener(delegate{OnMuteServerChanged();});
}
public override void OnStartLocalPlayer()
{
	managerScript=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerRefrences>().mngr;
}
public void OnMuteHeadSetsChanged()
{
	if(isServer)
	return;
     GetComponent<PlayerPC>().RpcOnMuteHeadSets(true);
}
public void OnMuteServerChanged()
{
   if(!isServer)
   return;
  if (!muteServer.isOn)
  {
	  managerScript.src.volume=1.0f;
  }
  else
  {
        managerScript.src.volume=0;
  }
}

#endregion
}

