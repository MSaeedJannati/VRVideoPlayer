using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;


public class CanvasListOfClients : NetworkBehaviour
{
    #region Variables;
    [SerializeField]
    int numberOfClients=2;
    public int[] Ids;
    public int[] tmpIds;
    public NamedClientStream namdClntStrmScrpt;
    #endregion
    #region Functions
    void Awake()
    {
            Ids = new int[numberOfClients];
            for (int i=0;i<Ids.Length;i++)
            {
                Ids[i] = 0;
            }
            tmpIds = new int[numberOfClients];
            for (int i = 0; i < tmpIds.Length; i++)
            {
                tmpIds[i] = 0;
            }
    }
    public void settmpIds(int num)
    {
        for (int i = 0; i < tmpIds.Length; i++)
        {
            tmpIds[i] = num;
        }
    }
    void Update()
    {
        if (!isServer)
            return;
        GameObject[] objs  = GameObject.FindGameObjectsWithTag("Player");
        settmpIds(0);
        for (int i=0;i<objs.Length;i++)
        {
          //  tmpIds[objs[i].GetComponent<PlayerConnectionEvents>().ID]=1;
        }
        for (int i=0;i<Ids.Length;i++)
        {
            if(Ids[i]!=tmpIds[i])
            {
                if (tmpIds[i] == 0)
                {
                    Disconnected(i);
                    Ids[i] = 0;
                }
            }
        }
    }
    public void addToLog(string input)
    {
    }
    public void Connected(int id)
    {
        if(id==0)
        return;
        Ids[id] = 1;
        addToLog(id+"Connected");
        Byte[] packet = new Byte[2];
        packet[0] = Convert.ToByte(1);
        packet[1] = Convert.ToByte(id);
        namdClntStrmScrpt.SendBytes(packet);
    }
    public void Disconnected(int id)
    {
        if(id==0)
        return;
        addToLog(id + "Disconnected");
        Byte[] packet = new Byte[2];
        packet[0] = Convert.ToByte(2);
        packet[1] = Convert.ToByte(id);
        namdClntStrmScrpt.SendBytes(packet);
    }
    public Byte ByteBuilder()
    {
        int a = 123;
        return Convert.ToByte(a);
    }
    #endregion
}