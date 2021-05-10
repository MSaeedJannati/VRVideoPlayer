using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerConnectionEvents : NetworkBehaviour
{
    #region Varibales
    [SyncVar]
    public string Name;
    [SyncVar]
    public int ID;
    public CanvasListOfClients cnvsScript;
    GameObject GameManager;
    PlayerXmlId xmlIdScript;
    string parsedData = "NUll";
    //------------------TcpConnection
    public int[] headSets;
    public int appNum;
    //----------Game Manager Ids
    GameManagerIdes gmMngrIds;

    PlayerPC plyrPcScrpt;
    #endregion
    #region Functions
    public void Awake()
    {
        headSets=new int[48];
        appNum=0;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        cnvsScript = GameManager.GetComponent<GameManagerRefrences1>().cnvsScrpt;
        xmlIdScript = GameManager.GetComponent<GameManagerRefrences1>().xmlIdScrpt;
        gmMngrIds=GameManager.GetComponent<GameManagerIdes>();
        plyrPcScrpt=GetComponent<PlayerPC>();
        
    }
    public override void OnStartLocalPlayer()
    {
        parsedData = xmlIdScript.OnPlayerNameAsked();
        CmdChangeName(parsedData);
        OnConnect();
        gameObject.name = Name;
        gmMngrIds.idOfLocalPlayer=int.Parse(Name);
    }
    public void OnDestroy()
    {
        CmdOnDisconnetc();
    }
    public void OnConnect()
    {
        CmdOnConnect();
    }
    public void OnApplicationQuit()
    {
        CmdOnDisconnetc();

    }
    void Update()
    {
        if (isLocalPlayer)
        {
            //Name = Application.platform + netId.ToString();
            CmdChangeName(parsedData);
        }
        gameObject.name = Name;
    }
    [Command]
    public void CmdOnDisconnetc()
    {
        //cnvsScript.addToLog(Name + "Disconnected");
    }
    [ClientRpc]
    public void RpcOnDisconnect()
    {

    }
    [Command]
    public void CmdOnConnect()
    {
        cnvsScript.Connected(ID);
    }
    [ClientRpc]
    public void RpcOnConnect()
    {

    }
   
    [Command]
    public void CmdChangeName(string inpt)
    {
        Name = inpt;
        int a = -1;
        int.TryParse(/*xmlIdScript.OnPlayerNameAsked()*/inpt,out a);
        ID = a;
    }
    #region StartApp
    public void OnStartApp()
    {
      CmdStartApp(headSets);

    }
    [Command]
    public void CmdStartApp(int[] headsets)
    {
       StartApp( headsets);
       RpcStartApp(headsets);
    }
    [ClientRpc]
    public void RpcStartApp(int[] headsets)
    {
          if(isServer)
          return;
          StartApp(headsets);
    }
    public void StartApp(int[] headsets)
    {
        plyrPcScrpt.OnPlayOrdered(headsets);
    }
    #endregion
    #region  HandleHeadSets
    public void OnHandleHeadSets(byte[] headsets,byte[] appnum)
    {
       CmdHandlHeadSets(headsets,appnum);
    }
    [Command]
    public void CmdHandlHeadSets(byte[] headsets,byte[] appnum)
    {
       handeleHeadsets(headsets,appnum);
       RpcHandleheadsets(headsets,appnum);
    }
    [ClientRpc]
    public void RpcHandleheadsets(byte[] headsets,byte[] appnum)
    {
        if(isServer)
        return;
        handeleHeadsets(headsets,appnum);
    }
    public void handeleHeadsets(byte[] headsets,byte[] appnum)
    {
      for(int i=0 ; i<6;i++)
      {
          int b =headsets[i];
          print(headsets[i]);
          for(int j=0;j<8;j++)
          {
              headSets[i*8+j]=b%2;
              b/=2;
              if(gmMngrIds.idOfLocalPlayer==((i*8+j)+1))
              {
                  gmMngrIds.playTheVideoFlag=headSets[i*8+j];
              }
          }
      }
    }
    #endregion
    #region StopApp
    public void OnStopApp()
    {
      CmdStopApp();
    }
    [Command]
    public void CmdStopApp()
    {
        StopApp();
        RpcStopApp();

    }
    [ClientRpc]
    public void RpcStopApp()
    {
        if(isServer)
        return;
        StopApp();
    }
    public void StopApp()
    {
        plyrPcScrpt.OnStopOrdered();
    }
    #endregion
    #endregion
}
