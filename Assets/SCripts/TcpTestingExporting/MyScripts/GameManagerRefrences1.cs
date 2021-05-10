using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerRefrences1 : MonoBehaviour
{
    #region Variables
    public NamedClientStream namedClientScript;
    public Text logText;
    public CanvasListOfClients cnvsScrpt;
    public PlayerXmlId xmlIdScrpt;
    #endregion
    void Start()
    {
        xmlIdScrpt = GetComponent<PlayerXmlId>();
    }
}
