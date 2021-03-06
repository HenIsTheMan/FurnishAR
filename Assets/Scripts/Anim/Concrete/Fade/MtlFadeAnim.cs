using FurnishAR.Math;
using UnityEngine;

namespace FurnishAR.Anim {
    internal sealed class MtlFadeAnim: AbstractFadeAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal bool shldResetToOG;

		private Color color;
		private Color colorOG;

		[HideInInspector, SerializeField]
		internal Material mtl;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal MtlFadeAnim(): base() {
			shldResetToOG = true;

			color = Color.white;
			colorOG = Color.white;

			mtl = null;
		}

        static MtlFadeAnim() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		protected override void OnDisable() {
			base.OnDisable();

			if(shldResetToOG && mtl != null) {
				mtl.color = colorOG;
			}
		}

		#endregion

		protected override void SubInitStuff() {
			color = mtl.color;
			colorOG = color;
			color.a = startAlpha;
			mtl.color = color;
		}

		protected override void UpdateAnim() {
			color.a = Val.Lerp(startAlpha, endAlpha, easingDelegate(x: Mathf.Min(1.0f, animTime / animDuration)));
			mtl.color = color;
		}
	}
}