using UnityEngine;
using static IWP.Game.GameUnitTypeFlags;

namespace IWP.Game {
	internal sealed class GameUnitAttribs: MonoBehaviour {
		#region Fields

		internal GameObject markerGO;

		internal GameUnitTypes gameUnitTypes;

		internal GameObject gridCellGO;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal GameUnitAttribs(): base() {
			markerGO = null;

			gameUnitTypes = GameUnitTypes.None;

			gridCellGO = null;
		}

        static GameUnitAttribs() {
        }

		#endregion

		#region Unity User Callback Event Funcs
		#endregion
	}
}