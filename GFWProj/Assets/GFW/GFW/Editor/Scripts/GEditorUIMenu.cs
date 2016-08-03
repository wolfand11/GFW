using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

namespace GFW
{
	public class GEditorUIMenu : ScriptableObject
	{
		[MenuItem ("GFW/UI/GScene")]  
		static void MenuAddGScene ()
		{
			const string nodeName = "__Scene__";
			if (!GameObject.Find (nodeName)) {
				GameObject obj = GEditorUtility.CreateRootGameObject (nodeName);	
				obj.AddComponent (typeof(MonoBehaviour));
			} else {
				GLogUtility.LogWarn ("exist " + nodeName);
			}
		}

		[MenuItem ("GFW/UI/GMovingImage")]  
		static void MenuAddGMovingImage ()
		{
			const string rootName = "GameObject_mvImage";
			const string nodeName = GMovingImage.SubImgName;
			Transform[] transforms = Selection.GetTransforms (SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
			if (transforms.Length > 0) {
				var selectNode = transforms [0].gameObject;
				var canvas = selectNode.GetComponentInParent (typeof(Canvas));
				if (canvas != null) {
					GameObject root = GCoordUtility.CreateFullScreenUINode (selectNode, rootName);
					GMovingImage mvImgComp = root.AddComponent (typeof(GMovingImage)) as GMovingImage;
					GameObject node = GCoordUtility.CreateFullScreenUINode (root, nodeName);
					Image imgComp = node.AddComponent (typeof(Image)) as Image;
					mvImgComp.imgTemplate = imgComp;
				} else {
					GLogUtility.LogWarn ("GMovingImage should be a child of Canvas!");
				}
			}
		}
	}
}

