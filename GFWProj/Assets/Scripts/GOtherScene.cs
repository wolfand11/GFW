using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GOtherScene : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		var btnChangeScene = GameObject.Find ("Button_changeScene").GetComponent<Button> ();
		btnChangeScene.onClick.AddListener (OnPressedChangeScene);
	}

	void OnPressedChangeScene ()
	{
		GSceneMgr.GetInstance ().ChangeToNextScene ();
	}
}
