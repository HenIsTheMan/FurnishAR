using UnityEngine;

namespace IWP.Anim {
    internal abstract class AbstractRotateAnim: AbstractAnim {
		#region Fields

		protected Vector3 myEulerAngles;

		[HideInInspector, SerializeField]
		internal Vector3 startEulerAngles;

		[HideInInspector, SerializeField]
		internal Vector3 endEulerAngles;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal AbstractRotateAnim(): base() {
			myEulerAngles = Vector3.zero;
			startEulerAngles = Vector3.zero;
			endEulerAngles = Vector3.zero;
		}

        static AbstractRotateAnim() {
        }

        #endregion

        #region Unity User Callback Event Funcs
        #endregion
    }
}