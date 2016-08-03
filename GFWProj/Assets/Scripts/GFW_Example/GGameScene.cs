using UnityEngine;
using System.Collections;
using GFW;

public class GGameScene : GSceneBase
{
	// Use this for initialization
	public override void OnGStart (bool isFirst)
	{
		if (isFirst) {
			GGameModal.GetInstance ().CurState = GGameModal.GEGameState.kWaitStart;
			GUIViewMgr.GetInstance ().PushView (GGameView.CreateView, GViewZOrder.kZOrderMinus1);
			GUIViewMgr.GetInstance ().PushView (GGameStartView.CreateView);
		}
	}
}


