using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstTimeLogIn : MonoBehaviour {

#region Variables
public string Password="Goplayer98";
public InputField field;
public GameObject errorPanel;
public int isLoggedIn;
#endregion
#region properties
#endregion
#region Functions
public void Start()
{
	field.inputType=InputField.InputType.Password;
	//Invoke("ChangeScene",2);
	ChangeScene();
}
public void resetPass()
{
	PlayerPrefs.SetInt("isLoggedIn",0);
}
public void OnValueChanged(InputField fld)
{
   if(fld.text==Password)
   {
	PlayerPrefs.SetInt("isLoggedIn",1);
	SceneManager.LoadScene(1,LoadSceneMode.Single);

   }
   else
   {
    errorPanel.SetActive(true);
   }
}
public void OkClicked()
{
	errorPanel.SetActive(false);
}
public void ChangeScene()
{
	isLoggedIn=PlayerPrefs.GetInt("isLoggedIn",0);
if(isLoggedIn==1)
	{
		SceneManager.LoadScene(1,LoadSceneMode.Single);
	}
}
#endregion
}
