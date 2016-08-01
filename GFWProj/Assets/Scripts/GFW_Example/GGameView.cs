using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GFW;

public class GGameView : MonoBehaviour,IPointerDownHandler
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

	private int curScore_ = 0;
	private float birdForce_ = 1500.0f;

	private GEventTrigger startTrigger;

	void Start ()
	{
		SetIsOnlyBg ();

		btnResume.onClick.AddListener (OnPressedResume);
		btnPause.onClick.AddListener (OnPressedPause);
		objBird.GetComponent<GBirdController> ().eventDied += OnFailed;
	}

	void OnEnable ()
	{
		startTrigger = GEventMgr.GetInstance ().Register ((int)GEventType.kEvent_PressStart,
			OnPressedStart);
	}

	void OnDisable ()
	{
		if (startTrigger != null) {
			startTrigger.detach ();
		}
	}

	public void OnPressedStart (int eventType, params Object[] args)
	{
		SetIsTutorial (true);
	}

	void SetIsOnlyBg ()
	{
		objOtherRoot.SetActive (false);
		objBird.SetActive (false);
		objBird.GetComponent<GMoveScript> ().enabled = false;
		objBird.GetComponent<Rigidbody2D> ().isKinematic = true;
		imgTutorial.gameObject.SetActive (false);
	}

	void SetIsPlaying ()
	{
		imgTutorial.gameObject.SetActive (false);
		objOtherRoot.SetActive (true);
		objBird.GetComponent<Animator> ().Play ("blue_fly");
		objBird.GetComponent<GMoveScript> ().enabled = true;
		objBird.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	void SetIsTutorial (bool isTutorial)
	{
		objBird.SetActive (isTutorial);
		objBird.GetComponent<Animator> ().Stop ();
		imgTutorial.gameObject.SetActive (isTutorial);

		objOtherRoot.SetActive (!isTutorial);
	}

	void SetPauseOrResume (bool isPause)
	{
		btnResume.gameObject.SetActive (isPause);
		btnPause.gameObject.SetActive (!isPause);
	}

	void UpdateScore ()
	{
		txtScore.text = "<b>SCORE: </b>" + curScore_.ToString ();
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

		GLogUtility.LogInfo ("OnPressedResume!");
		Time.timeScale = 1.0f;
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPlaying;
	}

	void OnFailed ()
	{
		GLogUtility.LogInfo ("OnFailed!");
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		GGameModal.GEGameState curState = GGameModal.GetInstance ().CurState;
		if (curState == GGameModal.GEGameState.kToturial) {
			SetIsPlaying ();
			GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPlaying;
		} else if (curState == GGameModal.GEGameState.kPlaying) {
			objBird.GetComponent<GMoveScript> ().CurForceOrSpeed = birdForce_;
		}
	}

	public static GameObject CreateView ()
	{
		var obj = Resources.Load ("UI/Root_game") as GameObject;
		return  Instantiate (obj);
	}
}
