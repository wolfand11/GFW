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
		GLogUtility.LogInfo ("OnPressedPlayAgain!");
	}

	public static GameObject CreateView ()
	{
		return Resources.Load ("UI/Root_gameResult.prefab") as GameObject;
	}
}
