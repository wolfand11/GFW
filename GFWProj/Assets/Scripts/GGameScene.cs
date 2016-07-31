using UnityEngine;
using System.Collections;
using GFW;

public class GGameScene : MonoBehaviour
{
	private GameObject startView_;
	private GameObject gameView_;
	// Use this for initialization
	void Start ()
	{
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kWaitStart;

		gameView_ = GUIViewMgr.GetInstance ().PushView (GGameView.CreateView, GViewZOrder.kZOrder_1);
		startView_ = GUIViewMgr.GetInstance ().PushView (GGameStartView.CreateView);
	}

	void OnPressedStart ()
	{
		GLogUtility.LogDebug ("GGameScene start game!");
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kPlaying;
	}
}


