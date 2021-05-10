using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerTImer : MonoBehaviour {
#region  Variables
[SerializeField]
XmlBasedDiscovery myDsicovery;
[SerializeField]
Text TimerText;
int crTime;
#endregion
#region Properties
#endregion
#region Functions
void Start()
{
TimerText=GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
TimerText.color=Color.red;
///TimerText.text="Name: "+SystemInfo.deviceModel+"  "+"Model:"+SystemInfo.deviceName+" UniqeID:"+SystemInfo.deviceUniqueIdentifier+" OpratingSystem:"+SystemInfo.operatingSystem+" OpFamily:"+SystemInfo.operatingSystemFamily;
}


public void SetText(int t)
{
     TimerText.text=t.ToString();
    ///TimerText.text="Name: "+SystemInfo.deviceModel+"  "+"Model:"+SystemInfo.deviceName+" UniqeID:"+SystemInfo.deviceUniqueIdentifier+" OpratingSystem:"+SystemInfo.operatingSystem+" OpFamily:"+SystemInfo.operatingSystemFamily;
}

public void SetText(string t)
{
     TimerText.text=t;
    ///TimerText.text="Name: "+SystemInfo.deviceModel+"  "+"Model:"+SystemInfo.deviceName+" UniqeID:"+SystemInfo.deviceUniqueIdentifier+" OpratingSystem:"+SystemInfo.operatingSystem+" OpFamily:"+SystemInfo.operatingSystemFamily;
}

#endregion
}
