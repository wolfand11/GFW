using UnityEngine;
using System.Collections;
using GFW;

public class GGameScene : MonoBehaviour
{
	static GameObject CreateBg ()
	{
		var obj = Resources.Load ("UI/Root_bg") as GameObject;
		return  Instantiate (obj);
	}

	enum GEGameState
	{
		kWaitStart,
		kPlaying,
		kFinished
	}

	private GEGameState curState_;

	private GameObject startView_;
	private GameObject gameView_;
	// Use this for initialization
	void Start ()
	{
		curState_ = GEGameState.kWaitStart;

		GUIViewMgr.GetInstance ().PushView (CreateBg);
		startView_ = GUIViewMgr.GetInstance ().PushView (GGameStartView.CreateView, GViewZOrder.kZOrder2);

		startView_.GetComponent<GGameStartView> ().eventStart += OnPressedStart;
	}

	void OnPressedStart ()
	{
		GLogUtility.LogDebug ("GGameScene start game!");
		curState_ = GEGameState.kPlaying;


	}

	void OnMouseDown ()
	{
		
	}

	void Update ()
	{
		if (curState_ == GEGameState.kPlaying) {
			
		}
	}
}
