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
		kRigidbodySpeed,
		kRigidbodyForce,
		kNormalSpeed
	}

	public GEMoveType moveType;
	[SerializeField]
	private bool isEnableMove = true;

	public bool IsEnableMove {
		get{ return isEnableMove; }
		set {
			isEnableMove = value;

			if (moveType != GEMoveType.kNormalSpeed) {
				GetComponent<Rigidbody2D> ().isKinematic = true;
			}
		}
	}

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
				
			if (IsEnableMove) {
				if (moveType == GEMoveType.kRigidbodySpeed) {
					GetComponent<Rigidbody2D> ().velocity = tempForceOrSpeed_;	
				} else if (moveType == GEMoveType.kRigidbodyForce) {
					GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
					GetComponent<Rigidbody2D> ().AddForce (tempForceOrSpeed_);
				}
			}
		}
	}

	void OnValidate ()
	{
		CurForceOrSpeed = forceOrSpeed_;
		IsEnableMove = isEnableMove;
	}

	Vector3 deltaPosV3 = Vector3.zero;

	void Update ()
	{
		if (IsEnableMove && moveType == GEMoveType.kNormalSpeed) {
			if (moveDir == GEMoveDir.kHorizontal) {
				deltaPosV3.x = Time.deltaTime * forceOrSpeed_;
			} else if (moveDir == GEMoveDir.kVertical) {
				deltaPosV3.y = Time.deltaTime * forceOrSpeed_;
			}	
			transform.localPosition += deltaPosV3;
		}
	}
}
