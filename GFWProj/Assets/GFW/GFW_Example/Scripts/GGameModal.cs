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

	private int curScore_;

	public int CurScore {
		set{ curScore_ = value; }
		get{ return curScore_; }
	}

	public void Init ()
	{			
	}

	public void Finalize ()
	{
	}
}

