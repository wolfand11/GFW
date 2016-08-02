using UnityEngine;
using System.Collections;

namespace GFW
{
	public static class GAnimatorExtension
	{
		public static void PlayOrStopAnimator (this Animator animator, bool isPlay)
		{
			if (animator != null) {
				Animator[] animators = animator.GetComponentsInChildren<Animator> ();
				foreach (Animator temp in animators) {
					temp.enabled = isPlay;
				}	
			}
		}
	}
}
