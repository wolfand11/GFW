using UnityEngine;
using System.Collections;
using GFW;

// 1 调用顺序如下
// OnValidate -> OnValidate-> Awake -> OnEnable -> Start -> FixedUpdate -> Update
// -> LateUpdate -> (OnPreRender -> OnPostRender -> OnRenderImage)
// -> OnDisable -> OnDestroy -> OnValidate
// 2 只有Camera下的脚本会触发 OnPreRender -> OnPostRender -> OnRenderImage
// 3 只有在编辑器模式下才会触发 OnValidate
public class MonoBehaviourTest : MonoBehaviour
{
	public static int counter = 1;

	// 脚本实例被加载时调用
	// Awake is called when the script instance is being loaded.
	public void Awake ()
	{
		GLogUtility.LogInfo (string.Format ("{0} Awake", counter++));
	}

	// 当脚本为enabled，在任何Update方法第一次调用之前，会调用Start
	// Start is called on the frame when a script is enabled just before any of
	// the Update methods is called the first time.
	public void Start ()
	{
		GLogUtility.LogInfo (string.Format ("{0} Start", counter++));
	}

	// 重置默认值，将脚本组件删除，再添加时会调用
	// Reset to default values.
	public void Reset ()
	{
		GLogUtility.LogInfo (string.Format ("{0} Reset", counter++));
	}

	// 当对象变为enabled或active时，调用OnEnable
	//This function is called when the object becomes enabled and active.
	public void OnEnable ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnEnable", counter++));
	}

	// Monobehaviour 变为Disabled或Inactive时，会调用OnDisable
	// This function is called when the behaviour becomes disabled () or inactive.
	public void OnDisable ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnDisable", counter++));
	}

	// MonoBehaviour 被销毁时会调用OnDestroy
	// This function is called when the MonoBehaviour will be destroyed.
	public void OnDestroy ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnDestroy", counter++));
	}

	// 当脚本加载 或者 inspector面板上的值被修改时会调用 OnValidate
	//This function is called when the script is loaded or a value is
	//changed in the inspector (Called in the editor only).
	public void OnValidate ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnValidate", counter++));
	}

	// OnPreRender is called before a camera starts rendering the scene.
	public void OnPreRender ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnPreRender", counter++));
	}
		
	// OnPostRender is called after a camera finished rendering the scene.
	public void OnPostRender ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnPostRender", counter++));
	}

	// OnRenderImage is called after all rendering is complete to render image.
	public void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		GLogUtility.LogInfo (string.Format ("{0} OnRenderImage", counter++));
	}

	// 如果物体是可见的，每个摄像机触发一次OnWillRenderObject的调用
	// OnWillRenderObject is called once for each camera if the object is visible.
	public void OnWillRenderObject ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnWillRenderObject", counter++));
	}

	// 当前的collider/rigidbody和另外的collider/rigidbody开始接触时，会调用OnCollisionEnter
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another
	// rigidbody/collider.
	public void OnCollisionEnter ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnCollisionEnter", counter++));
	}

	// 当前的collider/rigidbody和另外的collider/rigidbody停止接触时，会调用OnCollisionExit
	// OnCollisionExit is called when this collider/rigidbody has stopped touching another
	// rigidbody/collider.
	public void OnCollisionExit	()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnCollisionExit", counter++));
	}

	// 每一个和其他collider/rigidbody有接触的collider/rigidbody都会在每帧中触发OnCollisionStay的调用
	// OnCollisionStay is called once per frame for every collider/rigidbody that is touching
	// rigidbody/collider.
	public void OnCollisionStay ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnCollisionStay", counter++));
	}

	public void OnTriggerEnter ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnTriggerEnter", counter++));
	}

	public void OnTriggerExit ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnTriggerExit", counter++));
	}

	public void OnTriggerStay ()
	{
		GLogUtility.LogInfo (string.Format ("{0} OnTriggerStay", counter++));
	}

	// 如果MonoBehaviour状态为enabled，则以固定的帧率调用此函数
	//This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	public void FixedUpdate ()
	{
		GLogUtility.LogInfo (string.Format ("{0} FixedUpdate", counter++));
	}

	// 如果MonoBehaviour状态为enabled，则以固定的帧率调用此函数
	// Update is called every frame, if the MonoBehaviour is enabled.
	public void Update ()
	{
		GLogUtility.LogInfo (string.Format ("{0} Update", counter++));
	}

	// 如果MonoBehaviour状态为enabled，则以每帧都会调用此函数
	// LateUpdate is called every frame, if the Behaviour is enabled.
	public void LateUpdate ()
	{
		GLogUtility.LogInfo (string.Format ("{0} LateUpdate", counter++));
	}

}
