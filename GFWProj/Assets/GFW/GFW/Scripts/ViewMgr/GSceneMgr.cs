using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GFW;

namespace GFW
{
	public class GSceneMgr : GSingleton<GSceneMgr>
	{
		#region SceneMgr

		private GESceneType curLoadingScene_ = GESceneType.kMinInvalidScene;
		private GESceneType curScene_ = GESceneType.kMinInvalidScene;

		public GESceneType CurScene {
			get{ return curScene_; }
		}

		public class GSceneInfo
		{
			public GESceneType type;
			public string name;
			public Type sceneMgrType;
			public GViewStackGroup modalViewGroup = new GViewStackGroup (GViewType.kModalView);
			public GViewStackGroup uiViewGroup = new GViewStackGroup (GViewType.kUIView);
		}

		private Dictionary<GESceneType,GSceneInfo> sceneInfoMap_ = new Dictionary<GESceneType,GSceneInfo> ();

		public void RegisterScene (GESceneType type, string name, Type sceneMgrType)
		{
			if (!sceneInfoMap_.ContainsKey (type)) {
				GSceneInfo info = new GSceneInfo ();
				info.type = type;
				info.name = name;
				info.sceneMgrType = sceneMgrType;
				sceneInfoMap_.Add (type, info);
			} else {
				GLogUtility.LogError ("exist type = " + type.ToString ()
				+ " name = " + sceneInfoMap_ [type].name);
				GLogUtility.LogError ("current add type = " + type.ToString ()
				+ " name = " + name);
			}
		}

		public bool IsSceneValid (GESceneType sceneType)
		{
			return sceneInfoMap_.ContainsKey (sceneType);
		}

		public string GetSceneName (GESceneType sceneType)
		{
			if (sceneInfoMap_.ContainsKey (sceneType)) {
				return sceneInfoMap_ [sceneType].name;
			}
			GLogUtility.LogError ("Not exist scene type " + sceneType.ToString ());
			return "";
		}

		public GESceneType GetSceneType (string name)
		{
			foreach (var kvp in sceneInfoMap_) {
				if (kvp.Value.name == name) {
					return kvp.Value.type;
				}
			}
			return GESceneType.kMinInvalidScene;
		}

		void UnloadScene_ (string sceneName)
		{
			var sceneObj = GameObject.Find (GetSceneRootObjName ()) as GameObject;
			if (sceneObj) {
				GameObject.Destroy (sceneObj);
			}

			#if UNITY_EDITOR
			// do nothing
			#else
			SceneManager.UnloadScene (sceneName);
			#endif
			GLogUtility.LogInfo ("UnloadScene Completed " + sceneName);
		}

		void LoadScene_ (string sceneName)
		{
			SceneManager.LoadScene (sceneName);
		}

		public void OnLoadSceneCompleted (bool isFirst)
		{
			string sceneName = SceneManager.GetActiveScene ().name;
			if (curScene_ == GESceneType.kMinInvalidScene ||
			    curScene_ == GESceneType.kMaxInvalidScene) {
				curScene_ = GetSceneType (sceneName);
			} else {
				curScene_ = curLoadingScene_;

				isFirst = curScene_ == curLoadingScene_;
			}
			if (IsSceneValid (curScene_)) {
				GModalViewMgr.GetInstance ().CurViewGroup = sceneInfoMap_ [curScene_].modalViewGroup;
				GUIViewMgr.GetInstance ().CurViewGroup = sceneInfoMap_ [curScene_].uiViewGroup;
				InitScene_ (isFirst);
				if (!isFirst) {
					GModalViewMgr.GetInstance ().ShowAllTopView ();
					GModalViewMgr.GetInstance ().ShowAllTopView ();
				}
			} else {
				GLogUtility.LogError ("scene is unregister! name = " + sceneName);
			}
		}

		public void ChangeToScene (GESceneType sceneType, bool checkSameScene = false)
		{
			if (IsSceneValid (sceneType)) {
				if (checkSameScene && sceneType == curScene_) {
					return;
				}

				if (sceneType == curScene_) {
					GModalViewMgr.GetInstance ().EmptyAllStack ();
					GUIViewMgr.GetInstance ().EmptyAllStack ();
				}

				var sceneName = GetSceneName (curScene_);
				if (curScene_ != GESceneType.kMinInvalidScene &&
				    curScene_ != GESceneType.kMaxInvalidScene) {
					UnloadScene_ (sceneName);
				}

				curLoadingScene_ = sceneType;
				sceneName = GetSceneName (curLoadingScene_);
				LoadScene_ (sceneName);
			} else {
				GLogUtility.LogError ("Not exist scene type " + sceneType.ToString ());
			}
		}

		public void ChangeToNextScene ()
		{
			bool isChangeToNext = false;
			foreach (var kvp in sceneInfoMap_) {
				if (kvp.Key == curScene_) {
					isChangeToNext = true;
					continue;
				}
				if (isChangeToNext) {
					ChangeToScene (kvp.Key);
					return;
				}
			}
			if (isChangeToNext) {
				foreach (var kvp in sceneInfoMap_) {
					ChangeToScene (kvp.Key);
					return;
				}	
			}
		}

		#endregion

		// Canvas
		// - __UIView__
		// --  __UIView_1__
		// --  __UIView_2__
		// --  __UIView_3__
		// - __ModalView__
		// --  __ModalView_1__
		// --  __ModalView_2__
		// --  __ModalView_3__
		// __MainMgr__

		#region ViewMgr

		private const string uiCanvasName_ = "Canvas";
		private const string sceneRootName_ = "__Scene__";

		public static string GetSceneRootObjName ()
		{
			return sceneRootName_;
		}

		string GetViewRootName (GViewType type)
		{
			switch (type) {
			case GViewType.kModalView:
				return "__ModalView__";
			case GViewType.kUIView:
				return "__UIView__";
			}
			GLogUtility.LogError ("not exist view type. type = " + type.ToString ());
			return "";
		}

		string GetSubViewRootName (GViewType type, GViewZOrder zOrder)
		{
			switch (type) {
			case GViewType.kModalView:
				return "__ModalView_" + ((int)zOrder).ToString () + "__";
			case GViewType.kUIView:
				return "__UIView_" + ((int)zOrder).ToString () + "__";
			}
			GLogUtility.LogError ("not exist view type. type = " + type.ToString ());
			return "";
		}

		void InitScene_ (bool isFirst)
		{
			foreach (GViewType type in (GViewType[])GViewType.GetValues(typeof(GViewType))) {
				InitView_ (type);
			}

			InitSceneObj_ (isFirst);
		}

		void InitCanvas ()
		{
			// do nothing
		}

		void InitView_ (GViewType type)
		{
			var canvas = GameObject.Find (uiCanvasName_);
			if (canvas != null) {
				var viewRootName = GetViewRootName (type);
				var viewRoot = GCoordUtility.CreateFullScreenUINode (canvas.gameObject, viewRootName);
				var zOrderEnums = GViewZOrder.GetValues (typeof(GViewZOrder));
				Array.Sort (zOrderEnums);
				foreach (GViewZOrder zOrder in zOrderEnums) {
					string name = GetSubViewRootName (type, zOrder);
					GCoordUtility.CreateFullScreenUINode (viewRoot, name);
				}
			} else {
				GLogUtility.LogError ("dont exist Canvas. scene name = "
				+ SceneManager.GetActiveScene ().name);
			}
		}

		void InitSceneObj_ (bool isFirst)
		{
			var sceneObj = GameObject.Find (sceneRootName_);
			if (sceneObj == null) {
				sceneObj = new GameObject (sceneRootName_);
				GameObject.DontDestroyOnLoad (sceneObj);
			}
			var comp = sceneObj.GetComponent (sceneInfoMap_ [curScene_].sceneMgrType);
			if (comp == null) {
				comp = sceneObj.AddComponent (sceneInfoMap_ [curScene_].sceneMgrType);
			}
			((GSceneBase)comp).OnGStart (isFirst);
		}

		public GameObject GetViewRoot (GViewType type, GViewZOrder zOrder = GViewZOrder.kZOrder0)
		{
			var canvas = GameObject.Find (uiCanvasName_);
			if (canvas != null) {
				var subViewName = GetSubViewRootName (type, zOrder);
				//TODO : optimize
				var subViewRoot = GUtility.FindChildInTree (canvas, subViewName);
				if (subViewRoot != null) {
					return subViewRoot;
				} else {
					GLogUtility.LogError ("dont exist subSubView. name = "
					+ subViewName);
				}
			} else {
				GLogUtility.LogError ("dont exist Canvas. scene name = "
				+ SceneManager.GetActiveScene ().name);
			}
			return null;
		}

		#endregion
	}
}


