using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class RectTransformScaleAnim: AbstractScaleAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal RectTransform myRectTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal RectTransformScaleAnim(): base() {
			myRectTransform = null;
		}

        static RectTransformScaleAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myRectTransform.localScale = startScale;
		}

		protected override void UpdateAnim() {
			myRectTransform.localScale = Val.Lerp(ref startScale, ref endScale, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}