using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class NetworkManagerFindIfServer : MonoBehaviour {
#region Variables
[SerializeField]
TextAsset xmlFile;
#endregion
#region Properties
#endregion
#region Functions
public bool IsServer()
{
  int a=PareseXmlData(xmlFile.text);
  if(a==1)
   return true;
  else
   return false;
}
public int PareseXmlData(string xmlData)
{
  XmlDocument xmlDoc=new XmlDocument();
  xmlDoc.Load(new StringReader(xmlData));
  string xmlXpathPattern="//Devices/IsServer";
  XmlNode myNode=xmlDoc.SelectSingleNode(xmlXpathPattern);
 int a=3;
 int.TryParse(myNode.InnerText,out a) ;
 return a;
}
#endregion
}
