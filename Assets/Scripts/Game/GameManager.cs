using IWP.Generic;
using UnityEngine;
using static IWP.Generic.InitIDs;

namespace IWP.Game {
    internal sealed class GameManager: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		[SerializeField]
		private HexGridManager hexGridManager;

		[SerializeField]
		private float hexGridSpawnDelay;

		private WaitForSeconds hexGridWaitForSeconds;

		[SerializeField]
		private HexGridSpawner hexGridSpawner;

		[SerializeField]
		private GameUnitManager gameUnitManager;

		[SerializeField]
		private float gameUnitSpawnDelay;

		private WaitForSeconds gameUnitWaitForSeconds;

		[SerializeField]
		private GameUnitSpawner gameUnitSpawner;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal GameManager(): base() {
			initControl = null;

			hexGridManager = null;
			hexGridSpawnDelay = 0.0f;
			hexGridWaitForSeconds = null;
			hexGridSpawner = null;

			gameUnitManager = null;
			gameUnitSpawnDelay = 0.0f;
			gameUnitWaitForSeconds = null;
			gameUnitSpawner = null;
		}

        static GameManager() {
        }

        #endregion

        #region Unity User Callback Event Funcs

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.GameManager, Init);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.GameManager, Init);
		}

		#endregion

		private void Init() {
			if(!Mathf.Approximately(hexGridSpawnDelay, 0.0f)) {
				hexGridWaitForSeconds = new WaitForSeconds(hexGridSpawnDelay);
			}
			if(!Mathf.Approximately(gameUnitSpawnDelay, 0.0f)) {
				gameUnitWaitForSeconds = new WaitForSeconds(gameUnitSpawnDelay);
			}

			hexGridManager.InitMe();
			gameUnitManager.InitMe();

			hexGridSpawner.InitMe();
			gameUnitSpawner.InitMe();

			_ = StartCoroutine(nameof(Spawn));
		}

		private System.Collections.IEnumerator Spawn() {
			if(hexGridWaitForSeconds != null) {
				yield return hexGridWaitForSeconds;
			}

			hexGridSpawner.Spawn("SpawnHexGridForSinglePlayer");

			if(gameUnitWaitForSeconds != null) {
				yield return gameUnitWaitForSeconds;
			}

			gameUnitSpawner.Spawn("SpawnGameUnitsForSinglePlayer");
		}
	}
}