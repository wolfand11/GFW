using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GGamePlayUI : MonoBehaviour
{
	public Button btnPause;
	public Text txtScore;
	// Use this for initialization
	void Start ()
	{
		btnPause.onClick.AddListener (OnPressedPause);
	}

	void OnPressedPause ()
	{
		GLogUtility.LogInfo ("OnPressedPause!");
	}

	public static GameObject CreateView ()
	{
		return Resources.Load ("UI/Root_game.prefab") as GameObject;
	}
}
