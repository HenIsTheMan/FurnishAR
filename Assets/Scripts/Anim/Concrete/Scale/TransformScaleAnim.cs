using IWP.Math;
using UnityEngine;

namespace IWP.Anim {
    internal sealed class TransformScaleAnim: AbstractScaleAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Transform myTransform;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal TransformScaleAnim(): base() {
			myTransform = null;
		}

        static TransformScaleAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		protected override void SubInitStuff() {
			myTransform.localScale = startScale;
		}

		protected override void UpdateAnim() {
			myTransform.localScale = Val.Lerp(ref startScale, ref endScale, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
		}
	}
}