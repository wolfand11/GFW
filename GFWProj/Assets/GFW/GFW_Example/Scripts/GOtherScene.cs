using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GOtherScene : GSceneBase
{
	public override void OnGStart (bool isFirst)
	{
		if (isFirst) {
			var btnChangeScene = GameObject.Find ("Button_changeScene").GetComponent<Button> ();
			btnChangeScene.onClick.AddListener (OnPressedChangeScene);
		}
	}

	void OnPressedChangeScene ()
	{
		GSceneMgr.GetInstance ().ChangeToNextScene ();
	}
}
