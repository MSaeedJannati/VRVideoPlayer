using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoConnect : MonoBehaviour {

	#region Variabels
	public string ServerAddress="192.168.1.100";
	[SerializeField]
	VideoPlayerManager plyrManager;
	
	[SerializeField]
	NetworkManager manager;
//	int lastConnection=0;
//	int deltaTime=0;
	#endregion
	#region Functions
	/*public void OnCLick()
	{
			Destroy(manager.gameObject);
		SceneManager.LoadScene(1,LoadSceneMode.Single);	
	}
	void Start()
	{
		lastConnection=(int)Time.time;
		deltaTime=0;
	} */
	public void FixedUpdate()
	{
		
		if(Application.platform==RuntimePlatform.WindowsEditor
		/* || Application.platform==RuntimePlatform.WindowsPlayer*/)
		{
			if(!manager.IsClientConnected()&& !NetworkServer.active)
			{
				manager.StartHost();
			}	
		}
		else
		{
			/* deltaTime=(int)Time.time-lastConnection;			 
					Debugger.text="IsConnected"+!noConnection+" SinceLastConnection"+
					deltaTime+" isclcnnctd:"+manager.IsClientConnected();*/
			if(!manager.IsClientConnected()&& !NetworkServer.active)
			{ 
			/* 	if(deltaTime>7)
				{
				     lastConnection=(int)Time.time;
					 deltaTime=0;
					 OnCLick();
					 return;
				}*/
				bool noConnection = (manager.client == null || manager.client.connection == null ||
                                 manager.client.connection.connectionId == -1);
				if(noConnection)
				{

                manager.networkAddress=ServerAddress;
				manager.StartClient();
				if(plyrManager.plyr.isPlaying)
				plyrManager.StopPlaying();
				} 
					
			}
			/* else
			{
				lastConnection=(int)Time.time;
			}*/
		}
		
	}

	#endregion

}
