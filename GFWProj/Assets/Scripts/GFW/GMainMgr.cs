using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GFW;

namespace GFW
{
	public enum GESceneType
	{
		kMinInvalidScene,
		kGameScene,

		kLoginScene,
		kMainScene,
		kMaxInvalidScene
	}

	public class GMainMgr
	{
		private static bool isInited = false;
		private const string mainMgrGObjName_ = "__MainMgr__";
		private static GameObject mainMgrGObj_ = null;

		public static string GetMainMgrGObjName ()
		{
			return mainMgrGObjName_;
		}

		public static GameObject GetMainMgrGObj ()
		{
			if (!isInited) {
				GLogUtility.LogError ("GMainMgr is not inited!");
			}
			return mainMgrGObj_;
		}

		private static GMainMgr instance_ = null;

		public static GMainMgr GetInstance ()
		{
			if (!isInited) {
				GLogUtility.LogError ("GMainMgr is not inited!");
			}
			return instance_;
		}

		[RuntimeInitializeOnLoadMethod]
		static void Init_ ()
		{
			if (!isInited) {
				isInited = true;
				// init self
				mainMgrGObj_ = GameObject.Find (mainMgrGObjName_);
				if (mainMgrGObj_ == null) {
					//TODO: support running don't need GMainMgr
//					GLogUtility.LogError ("not exist " + mainMgrGObjName_);
//					GAppUtility.Quit ();
					return;
				}
				mainMgrGObj_.AddComponent (typeof(GMainMgrMonoBehaviour));
				GameObject.DontDestroyOnLoad (mainMgrGObj_);

				// init scene
				GSceneMgr.GetInstance ().RegisterScene (GESceneType.kGameScene, "GameScene", typeof(GGameScene));
				GSceneMgr.GetInstance ().RegisterScene (GESceneType.kMainScene, "MainScene", typeof(GOtherScene));
				GSceneMgr.GetInstance ().RegisterScene (GESceneType.kLoginScene, "LoginScene", typeof(GOtherScene));


				GSceneMgr.GetInstance ().OnLoadSceneCompleted ();
				GLogUtility.LogInfo ("GMainMgr Init Completed!");
			}
		}

		static void Finalize_ ()
		{
		}

		// process global event
		class GMainMgrMonoBehaviour:MonoBehaviour
		{
			void OnLevelWasLoaded (int level)
			{
				Scene scene = SceneManager.GetActiveScene ();
				GLogUtility.LogInfo ("OnLevelWasLoaded name = " + scene.name);


				GSceneMgr.GetInstance ().OnLoadSceneCompleted ();
			}
		}
	}
}