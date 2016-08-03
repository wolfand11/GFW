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
	public GameObject objPipeRoot;
	public GameObject objPipe;
	public GameObject objOtherRoot;
	public Image imgTutorial;

	private int curScore_ = 0;
	private float birdForce_ = 2500.0f;
	private float pipeMoveSpeed;

	private GEventTrigger startTrigger;
	private GEventTrigger overTrigger;

	void Start ()
	{
		pipeMoveSpeed = mvImgBg2.GetComponent<GMovingImage> ().moveSpeed;

		SetIsOnlyBg ();

		btnResume.onClick.AddListener (OnPressedResume);
		btnPause.onClick.AddListener (OnPressedPause);

		GBirdController birdController = objBird.GetComponent<GBirdController> ();
		birdController.eventScore += AddScore;
	}

	void OnEnable ()
	{
		startTrigger = GEventMgr.GetInstance ().Register ((int)GEventType.kEvent_PressStart,
			OnPressedStart);
		overTrigger = GEventMgr.GetInstance ().Register ((int)GEventType.kEvent_GameOver,
			OnFailed);
	}

	void OnDisable ()
	{
		if (startTrigger != null) {
			startTrigger.detach ();
		}
		if (overTrigger != null) {
			overTrigger.detach ();
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
		GUtility.RemoveAllChildren (objPipeRoot);

		imgTutorial.gameObject.SetActive (false);
		objOtherRoot.SetActive (true);
		objBird.GetComponent<Animator> ().PlayOrStopAnimator (true);
		objBird.GetComponent<GMoveScript> ().enabled = true;
		objBird.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	void SetIsFailed ()
	{
		mvImgBg1.GetComponent<GMovingImage> ().moveSpeed = 0.0f;
		mvImgBg2.GetComponent<GMovingImage> ().moveSpeed = 0.0f;
		objBird.GetComponent<Animator> ().PlayOrStopAnimator (true);
		objBird.GetComponent<GMoveScript> ().enabled = true;
		objBird.GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	void SetIsTutorial (bool isTutorial)
	{
		objBird.SetActive (isTutorial);
		objBird.GetComponent<Animator> ().PlayOrStopAnimator (false);
		imgTutorial.gameObject.SetActive (isTutorial);

		objOtherRoot.SetActive (!isTutorial);
	}

	void SetPauseOrResume (bool isPause)
	{
		btnResume.gameObject.SetActive (isPause);
		btnPause.gameObject.SetActive (!isPause);
	}

	void SpawnPipe ()
	{
		GameObject pipeObj = Instantiate (objPipe);
		pipeObj.GetComponent<GPipeController> ().Init (objPipeRoot, pipeMoveSpeed);
	}

	void AddScore ()
	{
		curScore_ += 1;
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

	void OnFailed (int eventType, params Object[] args)
	{
		if (GGameModal.GetInstance ().CurState != GGameModal.GEGameState.kFinished) {
			SetIsFailed ();
			GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kFinished;
			GUIViewMgr.GetInstance ().PushView (GGameResultView.CreateView);
		}
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

	Vector2 tempBirdSpeed = Vector2.zero;
	Animator tempBirdAnimator = null;
	float spwanPipeInterval_ = 1.5f;
	float spwanPipeTimer_ = 0.0f;

	void Update ()
	{
		GGameModal.GEGameState curState = GGameModal.GetInstance ().CurState;
		if (curState == GGameModal.GEGameState.kPlaying) {
			tempBirdSpeed = objBird.GetComponent<Rigidbody2D> ().velocity;
			tempBirdAnimator = objBird.GetComponent<Animator> ();

			//Debug.Log ("speed " + tempBirdSpeed.ToString ());
			tempBirdAnimator.SetBool ("isFlyUp", false);
			tempBirdAnimator.SetBool ("isFlyDown", false);
			tempBirdAnimator.SetBool ("isFlyNormal", false);
			if (tempBirdSpeed.y > 5) {
				//tempBirdAnimator.SetTrigger ("flyUp");
				tempBirdAnimator.SetBool ("isFlyUp", true);
			} else if (tempBirdSpeed.y < 0) {
				//tempBirdAnimator.SetTrigger ("flyDown");
				tempBirdAnimator.SetBool ("isFlyDown", true);
			} else {
				//tempBirdAnimator.SetTrigger ("flyNormal");
				tempBirdAnimator.SetBool ("isFlyNormal", true);
			}

			spwanPipeTimer_ += Time.deltaTime;
			if (spwanPipeTimer_ >= spwanPipeInterval_) {
				SpawnPipe ();
				spwanPipeTimer_ -= spwanPipeInterval_;
			}
		}
	}

	public static GameObject CreateView ()
	{
		var obj = Resources.Load ("UI/Root_game") as GameObject;
		return  Instantiate (obj);
	}
}
