using System.Collections.Generic;
using UnityEngine;
using static FurnishAR.Game.GameUnitTypeFlags;

namespace FurnishAR.Game {
	internal sealed partial class GameUnitSpawner: MonoBehaviour {
		internal struct SpawnGameUnitsParams {
			internal int indexOffset;

			internal GameUnitTypes gameUnitTypes;
			internal List<GameObject> gameUnitGOs;

			internal GameObject gameUnitPrefab;
			internal float gameUnitScaleFactor;

			internal GameObject gameUnitMarkerPrefab;
			internal float gameUnitMarkerScaleFactor;
		};
	}
}