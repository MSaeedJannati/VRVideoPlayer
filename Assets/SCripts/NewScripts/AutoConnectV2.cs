using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AutoConnectV2 : MonoBehaviour {
#region variables
public string playerExistenceStatus;

//client version
public string serverAddress="192.168.1.100";

NetworkManager networkManager;
#endregion 
#region  functions
void Start()
{ 
	networkManager=GetComponent<NetworkManager>();
	StartCoroutine(CheckConnectionCouroutine());
}
public IEnumerator CheckConnectionCouroutine()
{
	while(true)
	{
   GameObject[] plyrs=GameObject.FindGameObjectsWithTag("Player");
		if(plyrs.Length==0)
		{
			// networkManager.StartHost();
			networkManager.networkAddress=serverAddress;
			networkManager.StartClient();
			print(networkManager.numPlayers);
		}
		else
		{
		}
		yield return new WaitForSeconds(2);
	}
}
#endregion 
}
