using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GFW;

namespace GFW
{
	public class GCoordUtility
	{
		public enum DirType
		{
			kHorizontal,
			kVertical
		}

		public static int GetViewLenInDir (DirType dirType)
		{
			if (dirType == DirType.kHorizontal) {
				return Screen.width;
			} else if (dirType == DirType.kVertical) {
				return Screen.height;
			} else {
				Debug.LogError (string.Format ("dirType({0}) error!", dirType));
				return 0;
			}
		}

		public static Vector2 GetCanvasReferenceResolution (GameObject gObj)
		{
			CanvasScaler scaler = GUtility.GetComponentInSelfAndParent<CanvasScaler> (gObj);
			return scaler.referenceResolution;
		}

		public static float GetCanvasReferenceResolutionLenInDir (DirType dirType, GameObject gObj)
		{
			Vector2 referenceResolution = GetCanvasReferenceResolution (gObj);
			if (dirType == DirType.kHorizontal) {
				return referenceResolution.x;
			} else if (dirType == DirType.kVertical) {
				return referenceResolution.y;
			} else {
				Debug.LogError (string.Format ("dirType({0}) error!", dirType));
				return 0;
			}
		}

		public static void ResetRectToFullScreenAndInMiddle (RectTransform rectTransform)
		{
			if (rectTransform != null) {
				rectTransform.pivot = new Vector2 (0.5f, 0.5f);
				rectTransform.localScale = Vector3.one;
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = Vector2.one;
				rectTransform.sizeDelta = Vector2.zero;
				rectTransform.localPosition = Vector3.zero;
			}
		}

		public static GameObject CreateFullScreenUINode (GameObject parentObj = null, string name = "")
		{
			var gameObj = new GameObject (name, typeof(RectTransform));
			if (parentObj != null) {
				gameObj.transform.SetParent (parentObj.transform);
			}
			ResetRectToFullScreenAndInMiddle (gameObj.GetComponent<RectTransform> ());
			return gameObj;
		}
	}
}

