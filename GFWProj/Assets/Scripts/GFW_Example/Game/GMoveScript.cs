using UnityEngine;
using GFW;

public class GMoveScript : MonoBehaviour
{
	public enum GEMoveDir
	{
		kHorizontal,
		kVertical
	}

	public GEMoveDir moveDir;

	public enum GEMoveType
	{
		kSpeed,
		kForce
	}

	public GEMoveType moveType;

	[SerializeField]
	private float forceOrSpeed_ = 0.0f;
	private Vector2 tempForceOrSpeed_ = Vector2.zero;

	public float CurForceOrSpeed {
		get{ return forceOrSpeed_; }
		set {
			forceOrSpeed_ = value;

			if (moveDir == GEMoveDir.kHorizontal) {
				tempForceOrSpeed_.x = forceOrSpeed_;
				tempForceOrSpeed_.y = 0;
			} else {
				tempForceOrSpeed_.x = 0;
				tempForceOrSpeed_.y = forceOrSpeed_;
			}

			if (moveType == GEMoveType.kSpeed) {
				GetComponent<Rigidbody2D> ().velocity = tempForceOrSpeed_;
			} else {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				GetComponent<Rigidbody2D> ().AddForce (tempForceOrSpeed_);
			}
		}
	}

	void OnValidate ()
	{
		CurForceOrSpeed = forceOrSpeed_;
	}
}
