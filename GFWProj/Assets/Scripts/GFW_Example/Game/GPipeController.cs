using UnityEngine;
using System.Collections;
using GFW;

public class GPipeController : MonoBehaviour
{
	public float minY = -344.0f;
	public float minDistance = 250.0f;
	public float maxDistance = 320.0f;
	private GEventTrigger overTrigger;

	public void Init (GameObject root, float speed)
	{
		if (root) {
			transform.SetParent (root.transform);
			GCoordUtility.ResetRectToZero (gameObject.GetComponent<RectTransform> ());
			Vector3 pos = Vector3.zero;
			pos.x = Random.Range (minDistance, maxDistance);
			pos.y = Random.Range (minY, 0);
			transform.localPosition = pos;

			GMoveScript moveScript_ = GetComponent<GMoveScript> ();
			moveScript_.CurForceOrSpeed = speed;
		}
	}

	void Update ()
	{
		if (transform.position.x < -20) {
			Destroy (gameObject);
		}
	}

	void OnEnable ()
	{
		overTrigger = GEventMgr.GetInstance ().Register ((int)GEventType.kEvent_GameOver,
			OnGameOver);
	}

	void OnDisable ()
	{
		if (overTrigger != null) {
			overTrigger.detach ();
		}
	}

	void OnGameOver (int eventType, params Object[] args)
	{
		GMoveScript moveScript_ = GetComponent<GMoveScript> ();
		moveScript_.CurForceOrSpeed = 0;
		moveScript_.IsEnableMove = false;
	}
}

