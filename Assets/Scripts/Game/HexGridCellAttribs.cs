using UnityEngine;
using static FurnishAR.Game.HexGridCellTypes;

namespace FurnishAR.Game {
    internal sealed class HexGridCellAttribs: MonoBehaviour {
		#region Fields 

		private HexGridCellType type;

		internal GameObject gameUnitOnMe;

		[SerializeField]
		internal MeshRenderer meshRenderer;

		#endregion

		#region Properties

		internal HexGridCellType Type {
			get {
				return type;
			}
			set {
				type = value;

				meshRenderer.material.SetTexture("_MainTex", HexGridManager.globalObj.cellTexs[(uint)type]);
			}
		}

		#endregion

		#region Ctors and Dtor

		internal HexGridCellAttribs(): base() {
			type = HexGridCellType.Amt;

			gameUnitOnMe = null;

			meshRenderer = null;
		}

        static HexGridCellAttribs() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion
	}
}