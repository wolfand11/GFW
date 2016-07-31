using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GGameStartView : MonoBehaviour
{
	public GameObject startRoot;
	public Button btnStart;
	public Button btnChangeScene;
	public Image imgTitle;

	// Use this for initialization
	void Start ()
	{
		btnStart.onClick.AddListener (OnPressedStart);
		btnChangeScene.onClick.AddListener (OnPressedChangeScene);
	}

	void OnPressedStart ()
	{
		GUIViewMgr.GetInstance ().PopView ();
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kToturial;
	}

	void OnPressedChangeScene ()
	{
		GSceneMgr.GetInstance ().ChangeToNextScene ();
	}

	public static GameObject CreateView ()
	{
		return GameObject.Instantiate (Resources.Load ("UI/Root_start") as GameObject);
	}
}
