using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GFW;

//[ExecuteInEditMode]
public class GMovingImage : MonoBehaviour
{
	public const string SubImgName = "__Image_mvImage__";
	public Image imgTemplate;
	private GCoordUtility.DirType preMoveDir;
	public GCoordUtility.DirType moveDir = GCoordUtility.DirType.kHorizontal;

	public GCoordUtility.DirType MoveDir {
		set {
			#if UNITY_EDITOR
			if (moveDir != preMoveDir) {
				if (!EditorApplication.isPlaying) {
					moveDir = value;
					preMoveDir = moveDir;
				} else {
					moveDir = preMoveDir;
					GLogUtility.LogWarn ("Will not take effect in time！");
				}
			}
			#else
			moveDir = value;
			#endif
		}
		get { return moveDir; }
	}

	public float moveSpeed = -100.0f;

	private RectTransform tile_ = null;
	private float tileLength = 0.0f;
	private float deltaPos = 0.0f;
	private float movedPos = 0.0f;
	private Vector3 deltaPosV3 = Vector3.zero;

	private void ResetData_ ()
	{
		if (imgTemplate != null) {
			tile_ = imgTemplate.GetComponent<RectTransform> ();
			GUtility.RemoveAllChildren (tile_.gameObject);

			movedPos = 0.0f;
			deltaPos = 0.0f;
			deltaPosV3 = Vector3.zero;
			tileLength = 0.0f;
		}
	}

	private void Init_ ()
	{
		CanvasScaler canvasScaler = GetComponentInParent<CanvasScaler> ();
		if (canvasScaler == null) {
			Debug.LogError ("cant find CanvasScaler from parent!");
			return;
		}

		Canvas canvas = canvasScaler.GetComponentInParent<Canvas> ();
		ResetData_ ();

		if (imgTemplate != null) {
			Vector3 pos = tile_.TransformPoint (tile_.rect.position) / canvas.scaleFactor;
			Vector3 size = tile_.rect.size;
			float partOneLength = 0.0f;
			float partTwoLength = 0.0f;
			if (moveDir == GCoordUtility.DirType.kHorizontal) {
				partOneLength = pos.x;
				tileLength = size.x;
			} else if (moveDir == GCoordUtility.DirType.kVertical) {
				partOneLength = pos.y;
				tileLength = size.y;
			}
			float screenLength = GCoordUtility.GetCanvasReferenceResolutionLenInDir (moveDir, gameObject);
			partTwoLength = screenLength - partOneLength - tileLength;

			//Debug.Log (string.Format ("partOneLength={0} partTwoLength={1} tileLength{2}",
			//partOneLength, partTwoLength, tileLength));

			int partOneCount = Mathf.CeilToInt (partOneLength / tileLength + 1);
			int partTwoCount = Mathf.CeilToInt (partTwoLength / tileLength + 1);

			//Debug.Log (string.Format ("partOneCount={0} partTwoCount={1}", partOneCount, partTwoCount));

			Vector3 tDeltaPos = new Vector3 ();
			if (moveDir == GCoordUtility.DirType.kHorizontal) {
				tDeltaPos.x = -tileLength;
			} else if (moveDir == GCoordUtility.DirType.kVertical) {
				tDeltaPos.y = -tileLength;
			}

			for (int i = 1; i <= partOneCount; i++) {
				AddGameObject (tDeltaPos, i);
			}
			tDeltaPos = -tDeltaPos;
			for (int i = 1; i <= partTwoCount; i++) {
				AddGameObject (tDeltaPos, i);
			}
		} else {
			Debug.LogError ("Missed imgTemplate!");
		}
	}

	private void Finalize_ ()
	{
		
	}

	private void AddGameObject (Vector3 tDeltaPos, int index)
	{
		GameObject tempObj = Instantiate (tile_.gameObject);
		tempObj.name = SubImgName;
		tempObj.transform.SetParent (tile_.parent);
		RectTransform tRectTrans = tempObj.GetComponent<RectTransform> ();
		tRectTrans.localPosition = tile_.localPosition;
		tRectTrans.localScale = tile_.localScale;
		tRectTrans.localPosition += index * tDeltaPos;
	}

	public void OnEnable ()
	{
		//Debug.Log (string.Format ("{0} OnEnable", counter++));
		//Init_ ();
	}

	public void Start ()
	{
		//Debug.Log (string.Format ("{0} Start", counter++));
		Init_ ();
	}

	public void OnDistroy ()
	{
		Finalize_ ();
	}

	public void OnValidate ()
	{
		MoveDir = moveDir;
	}

	public void Update ()
	{
		deltaPos = Time.deltaTime * moveSpeed;
		movedPos += deltaPos;

		if (moveSpeed < 0) {
			if (movedPos <= -tileLength) {
				deltaPos += tileLength;
				movedPos += tileLength;
			}
		} else {
			if (movedPos >= tileLength) {
				deltaPos -= tileLength;
				movedPos -= tileLength;
			}
		}
			
		if (moveDir == GCoordUtility.DirType.kHorizontal) {
			deltaPosV3.x = deltaPos;
		} else if (moveDir == GCoordUtility.DirType.kVertical) {
			deltaPosV3.y = deltaPos;
		}

		transform.localPosition += deltaPosV3;
	}
}
