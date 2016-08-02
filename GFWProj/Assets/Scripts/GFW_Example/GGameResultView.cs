using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GGameResultView : MonoBehaviour
{
	public Image imgMedal;
	public Button btnPlayAgain;
	public Text txtScore;
	public Text txtBestScore;

	// Use this for initialization
	void Start ()
	{
		btnPlayAgain.onClick.AddListener (OnPressedPlayAgain);
	}

	void OnPressedPlayAgain ()
	{
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kWaitStart;
		GSceneMgr.GetInstance ().ChangeToScene (GESceneType.kGameScene);
	}

	public static GameObject CreateView ()
	{
		return GameObject.Instantiate (Resources.Load ("UI/Root_gameResult") as GameObject);
	}
}
