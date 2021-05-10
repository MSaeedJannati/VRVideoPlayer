using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmMngrPuseOnExit : MonoBehaviour {

  public void OnApplicationExit()
  {
	  IEnumerable<GameObject> plyrs = GameObject.FindGameObjectsWithTag("Player");
	  foreach(GameObject plyr in plyrs)
	  {
		  plyr.GetComponent<PlayerPC>().Quit();
	  }
  }
}
