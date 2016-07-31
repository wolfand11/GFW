using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GFW;

public class GGameStartView : MonoBehaviour
{
	public GameObject startRoot;
	public Button btnStart;
	public Button btnChangeScene;
	public Image imgTitle;
	public Image imgTutorial;

	public delegate void GEventStart ();

	public event GEventStart eventStart;
	// Use this for initialization
	void Start ()
	{
		startRoot.SetActive (true);
		imgTutorial.gameObject.SetActive (false);

		btnStart.onClick.AddListener (OnPressedStart);
		btnChangeScene.onClick.AddListener (OnPressedChangeScene);
	}

	void OnPressedStart ()
	{
		startRoot.SetActive (false);
		imgTutorial.gameObject.SetActive (true);

		eventStart ();
	}

	void OnPressedChangeScene ()
	{
		GSceneMgr.GetInstance ().ChangeToNextScene ();
	}

	public static GameObject CreateView ()
	{
		return GameObject.Instantiate (Resources.Load ("UI/Root_start") as GameObject);
	}
}
