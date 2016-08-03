using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using GFW;

namespace GFW
{
	public class GEditorUtility
	{
		static public GameObject CreateRootGameObject (string name, params Type[] compTypes)
		{
			GameObject obj = new GameObject ();
			foreach (Type compType in compTypes) {
				obj.AddComponent (compType);
			}
			Selection.activeGameObject = obj;
			obj.transform.localPosition = Vector3.zero;
			obj.name = name;
			return obj;
		}
	}
}
