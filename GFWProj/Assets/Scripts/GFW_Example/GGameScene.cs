using UnityEngine;
using System.Collections;
using GFW;

public class GGameScene : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kWaitStart;

		GUIViewMgr.GetInstance ().PushView (GGameView.CreateView, GViewZOrder.kZOrder_1);
		GUIViewMgr.GetInstance ().PushView (GGameStartView.CreateView);
	}
}


