using System.Collections.Generic;
using UnityEngine;

namespace IWP.Game {
    internal sealed class GameUnitManager: MonoBehaviour {
        #region Fields

		[SerializeField]
		private int myGameUnitCount;

		[SerializeField]
		private int opposingGameUnitCount;

		internal List<GameObject> myGameUnitGOs;
		internal List<GameObject> opposingGameUnitGOs;

        #endregion

        #region Properties
		#endregion

		#region Ctors and Dtor

		internal GameUnitManager(): base() {
			myGameUnitCount = 0;
			opposingGameUnitCount = 0;

			myGameUnitGOs = null;
			opposingGameUnitGOs = null;
		}

        static GameUnitManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		internal void InitMe() {
			myGameUnitGOs = new List<GameObject>(myGameUnitCount);
			opposingGameUnitGOs = new List<GameObject>(opposingGameUnitCount);

			for(int i = 0; i < myGameUnitCount; ++i) {
				myGameUnitGOs.Add(null);
			}

			for(int i = 0; i < opposingGameUnitCount; ++i) {
				opposingGameUnitGOs.Add(null);
			}
		}
	}
}