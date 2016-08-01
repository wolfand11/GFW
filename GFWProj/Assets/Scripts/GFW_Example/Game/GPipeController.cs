using UnityEngine;
using System.Collections;

public class GPipeController : MonoBehaviour
{
	private GMoveScript moveScript_;

	void Awake ()
	{
		moveScript_ = GetComponent<GMoveScript> ();
	}

	void Start ()
	{
		moveScript_.enabled = false;
	}
}

