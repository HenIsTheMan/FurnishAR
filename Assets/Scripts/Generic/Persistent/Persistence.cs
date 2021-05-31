using UnityEngine;
using static FurnishAR.Generic.InitIDs;

namespace FurnishAR.Generic {
	[DisallowMultipleComponent]
	internal sealed class Persistence: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal Persistence(): base() {
			initControl = null;
		}

        static Persistence() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.Persistence, Init);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.Persistence, Init);
		}

		#endregion

		private void Init() {
			DontDestroyOnLoad(gameObject);
		}
	}
}