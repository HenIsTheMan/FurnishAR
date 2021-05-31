using UnityEngine;
using static IWP.Game.HexGridCellTypes;

namespace IWP.Game {
	internal sealed partial class HexGridSpawner: MonoBehaviour {
		public void SpawnHexGridForSinglePlayer() {
			hexGridManager.cellWeights[(uint)HexGridCellType.Empty] = 70;

			hexGridManager.cellWeights[(uint)HexGridCellType.Gun] = 30;
			hexGridManager.cellWeights[(uint)HexGridCellType.Lightning] = 10;
			hexGridManager.cellWeights[(uint)HexGridCellType.Meteor] = 50;

			hexGridManager.cellWeights[(uint)HexGridCellType.Death] = 20;

			hexGridManager.cellWeights[(uint)HexGridCellType.Hypnosis] = 20;
			hexGridManager.cellWeights[(uint)HexGridCellType.Sleep] = 20;

			hexGridManager.cellWeights[(uint)HexGridCellType.Bubble] = 10;
			hexGridManager.cellWeights[(uint)HexGridCellType.Pogo] = 50;
			hexGridManager.cellWeights[(uint)HexGridCellType.Rage] = 30;

			hexGridManager.cellWeights[(uint)HexGridCellType.Fire] = 40;
			hexGridManager.cellWeights[(uint)HexGridCellType.Poison] = 40;

			hexGridManager.cellWeights[(uint)HexGridCellType.Cleansing] = 30;
			hexGridManager.cellWeights[(uint)HexGridCellType.Teleportation] = 40;
			hexGridManager.cellWeights[(uint)HexGridCellType.Tornado] = 30;

			SpawnHexGrid();
		}
	}
}