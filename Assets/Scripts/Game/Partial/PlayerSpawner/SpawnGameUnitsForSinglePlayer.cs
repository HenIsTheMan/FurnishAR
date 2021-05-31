using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Game.GameUnitTypeFlags;

namespace FurnishAR.Game {
	internal sealed partial class GameUnitSpawner: MonoBehaviour {
		public void SpawnGameUnitsForSinglePlayer() {
			SpawnGameUnitsParams myParams;
			ShuffleElements.Shuffle(indices);

			float meanHexScaleFactor = (hexGridManager.cellScaleX + hexGridManager.cellScaleY) * 0.5f;

			myParams.indexOffset = 0;

			myParams.gameUnitTypes = GameUnitTypes.MyTeam;
			myParams.gameUnitGOs = gameUnitManager.myGameUnitGOs;

			myParams.gameUnitPrefab = myGameUnitPrefab;
			myParams.gameUnitScaleFactor = meanHexScaleFactor * 0.5f;

			myParams.gameUnitMarkerPrefab = myGameUnitMarkerPrefab;
			myParams.gameUnitMarkerScaleFactor = meanHexScaleFactor * 0.25f;

			SpawnGameUnits(myParams);

			myParams.indexOffset = gameUnitManager.myGameUnitGOs.Count;

			myParams.gameUnitTypes = GameUnitTypes.OpposingTeam;
			myParams.gameUnitGOs = gameUnitManager.opposingGameUnitGOs;

			myParams.gameUnitPrefab = opposingGameUnitPrefab;
			myParams.gameUnitScaleFactor = meanHexScaleFactor * 0.5f;

			myParams.gameUnitMarkerPrefab = opposingGameUnitMarkerPrefab;
			myParams.gameUnitMarkerScaleFactor = meanHexScaleFactor * 0.25f;

			SpawnGameUnits(myParams);
		}
	}
}