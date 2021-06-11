using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class RectTransformAnchoredTranslateAnim: AbstractTranslateAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal RectTransform myRectTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformAnchoredTranslateAnim(): base() {
			myRectTransform = null;
		}

        static RectTransformAnchoredTranslateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myRectTransform.anchoredPosition = startPos;
		}

		protected override void UpdateAnim() {
			myRectTransform.anchoredPosition = Val.Lerp(ref startPos, ref endPos, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}