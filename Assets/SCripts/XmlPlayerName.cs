using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.Networking;
using UnityEngine.UI;
public class XmlPlayerName : NetworkBehaviour
{
  public List<GameObject> players;
    Text clientsText;
  WWW XMLRawFile;
   public string Address;
   [SyncVar]
   public string PlayerName;
    Transform myTransform;
   
  public override void OnStartLocalPlayer()
  {
  GetNetIdentity();
  SetIdentity();
  }
  void Awake()
  {
   myTransform=transform;
    if(Application.platform==RuntimePlatform.WindowsEditor/* ||Application.platform==RuntimePlatform.WindowsPlayer*/)
     {
        XMLRawFile=new WWW("file://"+"D:\\GoPlayer\\DeviceID.xml");
     }
     else
     {
          // XMLRawFile=new WWW("file://"+"//storage//emulated//0//GoPlayer//DeviceID.xml");
          XMLRawFile=new WWW("file://"+"D:\\GoPlayer\\DeviceIDTst.xml");
     }
   if(isServer)
   {
      GameObject.FindGameObjectWithTag("GameManager")
     .GetComponent<GameManagerRefrences>().Canvas.SetActive(true);
     clientsText=GameObject.FindGameObjectWithTag("GameManager")
     .GetComponent<GameManagerRefrences>().clientsTxt.GetComponent<Text>();
   }
        string data=XMLRawFile.text;
  }
     void Start()
    {
      if(isServer && isLocalPlayer)
      {
     GameObject.FindGameObjectWithTag("GameManager")
     .GetComponent<GameManagerRefrences>().Canvas.SetActive(true);
         clientsText=GameObject.FindGameObjectWithTag("GameManager")
     .GetComponent<GameManagerRefrences>().clientsTxt.GetComponent<Text>();
      }
     if(Application.platform==RuntimePlatform.WindowsEditor||Application.platform==RuntimePlatform.WindowsPlayer)
     {
        XMLRawFile=new WWW("file://"+"D:\\GoPlayer\\DeviceID.xml");
     }
     else
     {
           XMLRawFile=new WWW("file://"+"//storage//emulated//0//GoPlayer//DeviceID.xml");
     }
   
        string data=XMLRawFile.text;
    
    }

public string OnPlayerNameAsk()
{
    return(parseXmlFile(XMLRawFile.text));
}
  string  parseXmlFile(string xmlData)
  {
   XmlDocument xmlDoc=new XmlDocument();
   xmlDoc.Load (new StringReader(xmlData));
   string xmlPathPattern="//Devices/ID";
   XmlNode myNode=xmlDoc.SelectSingleNode(xmlPathPattern);
  return  myNode.InnerXml;
  }
  [Client]
  public void GetNetIdentity()
  {
     CmdTellServerMyName(OnPlayerNameAsk());
  }
  [Command]
  public void CmdTellServerMyName(string name)
  {
   PlayerName=name;
  }
 public void SetIdentity()
 {
   if(isLocalPlayer)
   {
     myTransform.name=PlayerName;
   }
   else
   {
     myTransform.name=OnPlayerNameAsk();
   }
 }
 void Update()
 {
 
   if(myTransform.name=="" || myTransform.name=="Player(Clone)")
   {
     SetIdentity();
   }
 updateClientsPanel();
   
 }
 public void updateClientsPanel()
 {
   if(!isLocalPlayer)
   return;
    if(!isServer)
    return;
   
   players.Clear();
   IEnumerable<GameObject> plyrs=GameObject.FindGameObjectsWithTag("Player");
   players.AddRange(plyrs);
   
   clientsText.text=string.Empty;
     foreach(GameObject obj in players)
     {
         clientsText.text+=obj.GetComponent<XmlPlayerName>().PlayerName+"\n"; 
     }
   
 }
}
