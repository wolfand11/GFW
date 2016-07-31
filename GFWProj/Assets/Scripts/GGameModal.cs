using UnityEngine;
using System.Collections;
using GFW;

public class GGameModal:GSingleton<GGameModal>
{
	public enum GEGameState
	{
		kWaitStart,
		kToturial,
		kPlaying,
		kPause,
		kFinished
	}

	private GEGameState curState_;

	public GEGameState CurState {
		set{ curState_ = value; }
		get{ return curState_; }
	}
}

