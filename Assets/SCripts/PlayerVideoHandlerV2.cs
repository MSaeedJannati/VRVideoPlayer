using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerVideoHandlerV2 : NetworkBehaviour {
	#region  Variabels
	VideoPlayerManager mngrScrpt;
	GameManagerRefrences mngrRefrences;

	#endregion
	#region Functions
	public override void OnStartLocalPlayer()
	{
       mngrRefrences=GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerRefrences>();
	}

	//-----------play the vidoe
	public void OnPlayVideo(int vidoeNum)
	{
        CmdPlayVideo(vidoeNum);
	}
	public void PlayVideo(int videoNum)
	{
		mngrRefrences.videoPlayer.Play();
	}
	[Command]
	public void CmdPlayVideo(int vidoeNum)
	{
      RpcPlayVideo(vidoeNum);
	}
	[ClientRpc]
	public void RpcPlayVideo(int vidoeNum)
	{
		if(isServer)
		return;

	}
	#endregion

}
