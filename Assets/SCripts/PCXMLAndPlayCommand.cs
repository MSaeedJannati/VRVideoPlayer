using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
public class PCXMLAndPlayCommand : MonoBehaviour
{
  WWW XMLRawFile;
   public string Address;
    void Start()
    {
     XMLRawFile=new WWW("file://"+Path.GetFullPath(".")+"\\VideoNumber.xml");
     string data=XMLRawFile.text;  
    }

public string OnVideoNameAsked()
{
    return(parseXmlFile(XMLRawFile.text));
}
  string  parseXmlFile(string xmlData)
  {
   XmlDocument xmlDoc=new XmlDocument();
   xmlDoc.Load (new StringReader(xmlData));
   string xmlPathPattern="//VideoNumbers/Number";
   XmlNode myNode=xmlDoc.SelectSingleNode(xmlPathPattern);
  return  myNode.InnerXml;
  }
 
}
