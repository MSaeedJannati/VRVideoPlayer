using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class PlayerXmlId : MonoBehaviour
{
    WWW XMLRawFile;
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            XMLRawFile = new WWW("file://" + "D:\\TCPTesting\\DeviceID1.xml");
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //Client Build
            //XMLRawFile = new WWW("file://" + "D:\\TCPTesting\\DeviceID2.xml");
            //ServerBuild
            XMLRawFile = new WWW("file://" + "D:\\TCPTesting\\DeviceID1.xml");
        }
        else if (Application.platform==RuntimePlatform.Android)
        {
            XMLRawFile = new WWW("file://" + "//storage//emulated//TCPTesting//DeviceID.xml");
        }
        string data = XMLRawFile.text;
    }

    public string OnPlayerNameAsked()
    {
        return (parseXmlFile(XMLRawFile.text));
    }
    string parseXmlFile(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));
        string xmlPathPattern = "//Devices/ID";
        XmlNode myNode = xmlDoc.SelectSingleNode(xmlPathPattern);
        return myNode.InnerXml;
    }

}
