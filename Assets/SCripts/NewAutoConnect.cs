
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewAutoConnect : MonoBehaviour {

	#region Variabels
	string ServerAddress="192.168.1.100";
	[SerializeField]
	VideoPlayerManager plyrManager;
	[SerializeField]
	NetworkManager manager;
	#endregion
	#region Properties
	#endregion
	#region Functions
	public void FixedUpdate()
	{
		
		if(Application.platform==RuntimePlatform.WindowsEditor|| Application.platform==RuntimePlatform.WindowsPlayer)
		{
			if(!manager.IsClientConnected()&& !NetworkServer.active)
			{
				manager.StartHost();
			}	
		}
		else
		{
			if(!manager.IsClientConnected()&& !NetworkServer.active)
			{ 
                manager.networkAddress=ServerAddress;
				manager.StartClient();
				if(plyrManager.plyr.isPlaying)
				plyrManager.StopPlaying();
					
			}
		}
	}
	#endregion
}
