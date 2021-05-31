using UnityEngine;

namespace IWP.Anim {
    internal abstract class AbstractScaleAnim: AbstractAnim {
		#region Fields

		[HideInInspector, SerializeField]
		internal Vector3 startScale;

		[HideInInspector, SerializeField]
		internal Vector3 endScale;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal AbstractScaleAnim(): base() {
			startScale = Vector3.one;
			endScale = Vector3.one;
		}

        static AbstractScaleAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}