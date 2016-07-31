using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GGameView : MonoBehaviour
{
	public GameObject mvImgBg1;
	public GameObject mvImgBg2;
	public Button btnPause;
	public Button btnResume;
	public Text txtScore;
	public GameObject objBird;
	public GameObject objPipe;
	public GameObject objOtherRoot;
	public Image imgTutorial;

	public delegate void GEventFailed ();

	public event GEventFailed eventFailed;

	void Start ()
	{
		SetIsOnlyBg ();

		btnResume.onClick.AddListener (OnPressedResume);
		btnPause.onClick.AddListener (OnPressedPause);
	}

	void SetIsOnlyBg ()
	{
		objOtherRoot.SetActive (false);
		imgTutorial.gameObject.SetActive (false);
	}

	void SetIsTutorial (bool isTutorial)
	{
		imgTutorial.gameObject.SetActive (isTutorial);
		objOtherRoot.SetActive (!isTutorial);
	}

	void SetPauseOrResume (bool isPause)
	{
		btnResume.gameObject.SetActive (isPause);
		btnPause.gameObject.SetActive (!isPause);
	}

	void OnPressedPause ()
	{
		SetPauseOrResume (true);
		GLogUtility.LogInfo ("OnPressedPause!");
		Time.timeScale = 0.0f;
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPause;
	}

	void OnPressedResume ()
	{
		SetPauseOrResume (false);

		GLogUtility.LogInfo ("OnPressedPause!");
		Time.timeScale = 1.0f;
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPlaying;
	}

	void OnFailed ()
	{
		if (eventFailed != null) {
			eventFailed ();	
		}
	}

	void Update ()
	{
		GGameModal.GEGameState curState = GGameModal.GetInstance ().CurState;
		switch (curState) {
		case GGameModal.GEGameState.kToturial:
			imgTutorial.gameObject.SetActive (true);
			if (Input.touchCount > 0) {
				imgTutorial.gameObject.SetActive (false);
				GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPlaying;
			}
			break;
		case GGameModal.GEGameState.kPlaying:
			if (Input.touchCount > 0) {
				
			}
			break;
		case GGameModal.GEGameState.kPause:
			break;
		case GGameModal.GEGameState.kFinished:
			break;
		}
	}

	public static GameObject CreateView ()
	{
		var obj = Resources.Load ("UI/Root_game") as GameObject;
		return  Instantiate (obj);
	}
}
