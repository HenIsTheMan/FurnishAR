using IWP.Math;
using UnityEngine;

namespace IWP.Anim {
    internal sealed class TransformTranslateAnim: AbstractTranslateAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Transform myTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformTranslateAnim(): base() {
			myTransform = null;
		}

        static TransformTranslateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myTransform.localPosition = startPos;
		}

		protected override void UpdateAnim() {
			myTransform.localPosition = Val.Lerp(ref startPos, ref endPos, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}