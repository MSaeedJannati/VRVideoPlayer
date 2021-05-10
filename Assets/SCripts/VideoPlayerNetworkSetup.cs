using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerNetworkSetup : NetworkBehaviour
{
    #region Variables
    
    
    GameObject myCamera;
    GameObject gameManager;
    [SyncVar]
    public int playerID;
    XmlPlayerName xmlNameScrpt;
    #endregion
    #region Methodes
   public override void OnStartLocalPlayer()
   {
       xmlNameScrpt=GetComponent<XmlPlayerName>();
       CmdSetPlyrId(int.Parse(xmlNameScrpt.OnPlayerNameAsk()));
       gameManager=GameObject.FindGameObjectWithTag("GameManager");
      myCamera=gameManager.GetComponent<GameManagerRefrences>().CamRig;
        myCamera.SetActive(true);
        if(isServer)
        {
           GameObject.FindGameObjectWithTag("Canvas").SetActive(true);
       // gameObject.name="Player Host";
        }
       // else
        //gameObject.name="Player "+gameObject.GetComponent<NetworkIdentity>().netId.ToString();
            // GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerRefrences>().Canvas.SetActive(true);
   }

 [Command]
 public void CmdSetPlyrId(int id)
 {
    playerID=id;
 }
    
    #endregion
}
