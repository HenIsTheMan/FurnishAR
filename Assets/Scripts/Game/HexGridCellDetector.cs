using UnityEngine;

namespace IWP.Game {
    internal sealed class HexGridCellDetector: MonoBehaviour {
		#region Fields

		[SerializeField]
		private HexGridCellAttribs hexGridCellAttribs;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal HexGridCellDetector(): base() {
			hexGridCellAttribs = null;
		}

        static HexGridCellDetector() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		private void OnTriggerEnter(Collider otherCollider) {
			if(otherCollider.transform.parent.name == "GameUnits") {
				hexGridCellAttribs.gameUnitOnMe = otherCollider.gameObject;
			}
		}

		private void OnTriggerExit(Collider otherCollider) {
			if(otherCollider.transform.parent.name == "GameUnits") {
				hexGridCellAttribs.gameUnitOnMe = null;
			}
		}
    }
}