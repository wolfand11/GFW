using UnityEngine;
using System.Collections;

public class GBirdController : MonoBehaviour
{
	private GMoveScript moveScript_;

	public delegate void GEventDied ();

	public GEventDied eventDied;

	void Awake ()
	{
		moveScript_ = GetComponent<GMoveScript> ();
	}

	void Start ()
	{
		moveScript_.enabled = false;
	}

	void OnTriggerEnter2D (Collider2D otherCollider)
	{
		if (eventDied != null) {
			eventDied ();
		}
	}
}
