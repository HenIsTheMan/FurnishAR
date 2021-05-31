using IWP.Anim;
using UnityEngine;
using static IWP.Game.HexGridCellTypes;
using static IWP.Math.EasingTypes;

namespace IWP.Game {
	internal sealed partial class GameUnitSpawner: MonoBehaviour {
		#region Fields

		[SerializeField]
		private GameUnitManager gameUnitManager;

		[SerializeField]
		private GameObject myGameUnitPrefab;

		[SerializeField]
		private GameObject opposingGameUnitPrefab;

		[SerializeField]
		private GameObject myGameUnitMarkerPrefab;

		[SerializeField]
		private GameObject opposingGameUnitMarkerPrefab;

		[SerializeField]
		private Transform parentTransform;

		[SerializeField]
		private HexGridManager hexGridManager;

		private int[] indices;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal GameUnitSpawner(): base() {
			gameUnitManager = null;

			myGameUnitPrefab = null;
			opposingGameUnitPrefab = null;
			myGameUnitMarkerPrefab = null;
			opposingGameUnitMarkerPrefab = null;

			parentTransform = null;

			hexGridManager = null;
			indices = System.Array.Empty<int>();
		}

        static GameUnitSpawner() {
        }

        #endregion

        #region Unity User Callback Event Funcs
		#endregion

		internal void InitMe() {
			int indicsLen = hexGridManager.gridCellGOs.Length;
			indices = new int[indicsLen];

			for(int i = 0; i < indicsLen; ++i) {
				indices[i] = i;
			}
		}

		internal void Spawn(string spawnMethodName) {
			if(spawnMethodName != string.Empty) {
				_ = GetType().GetMethod(spawnMethodName).Invoke(this, null);
			}
		}

		private void SpawnGameUnits(SpawnGameUnitsParams myParams) {
			int gameUnitCount = myParams.gameUnitGOs.Count;

			GameObject gameUnitGO;
			GameUnitAttribs gameUnitAttribs;

			TransformTranslateAnim[] translateAnimScripts;
			TransformTranslateAnim translateAnimScript;
			TransformRotateAnim rotateAnimScript;

			Vector3 pos;

			for(int i = 0; i < gameUnitCount; ++i) {
				gameUnitGO = Instantiate(myParams.gameUnitPrefab, Vector3.zero, Quaternion.identity, parentTransform);

				gameUnitGO.transform.localScale = Vector3.one * myParams.gameUnitScaleFactor;
				gameUnitGO.name = myParams.gameUnitPrefab.name;

				myParams.gameUnitGOs[i] = gameUnitGO;

				gameUnitAttribs = gameUnitGO.GetComponent<GameUnitAttribs>();

				gameUnitAttribs.gameUnitTypes |= myParams.gameUnitTypes;

				gameUnitAttribs.gridCellGO = hexGridManager.gridCellGOs[indices[i + myParams.indexOffset]];
				gameUnitAttribs.gridCellGO.GetComponent<HexGridCellAttribs>().Type = HexGridCellType.Empty;

				pos = gameUnitAttribs.gridCellGO.transform.localPosition;
				pos.z = pos.y;
				pos.y = 4.0f;

				gameUnitGO.transform.localPosition = pos;

				pos.y += 4.0f;

				gameUnitAttribs.markerGO = Instantiate(myParams.gameUnitMarkerPrefab, pos, Quaternion.Euler(180.0f, 0.0f, 0.0f), gameUnitGO.transform);

				gameUnitAttribs.markerGO.transform.localScale = Vector3.one * myParams.gameUnitMarkerScaleFactor;

				gameUnitAttribs.markerGO.name = myParams.gameUnitMarkerPrefab.name;

				translateAnimScripts = gameUnitAttribs.markerGO.GetComponents<TransformTranslateAnim>();

				translateAnimScript = translateAnimScripts[0];

				translateAnimScript.animDuration = 0.5f;
				translateAnimScript.startTimeOffset = 0.0f;
				translateAnimScript.periodicDelay = 1.5f;
				translateAnimScript.countThreshold = 0;

				translateAnimScript.myTransform = gameUnitAttribs.markerGO.transform;
				pos = gameUnitAttribs.markerGO.transform.localPosition;
				translateAnimScript.startPos = pos;
				pos.y += 1.0f;
				translateAnimScript.endPos = pos;
				translateAnimScript.easingType = EasingType.EaseInSine;

				translateAnimScript.InitMe();
				translateAnimScript.IsUpdating = true;

				translateAnimScript = translateAnimScripts[1];

				translateAnimScript.animDuration = 0.5f;
				translateAnimScript.startTimeOffset = 0.5f;
				translateAnimScript.periodicDelay = 1.5f;
				translateAnimScript.countThreshold = 0;

				translateAnimScript.myTransform = gameUnitAttribs.markerGO.transform;
				translateAnimScript.startPos = pos;
				pos.y -= 1.0f;
				translateAnimScript.endPos = pos;
				translateAnimScript.easingType = EasingType.EaseOutSine;

				translateAnimScript.InitMe();
				translateAnimScript.IsUpdating = true;

				translateAnimScript = translateAnimScripts[2];

				translateAnimScript.animDuration = 0.5f;
				translateAnimScript.startTimeOffset = 1.0f;
				translateAnimScript.periodicDelay = 1.5f;
				translateAnimScript.countThreshold = 0;

				translateAnimScript.myTransform = gameUnitAttribs.markerGO.transform;
				translateAnimScript.startPos = pos;
				pos.y += 2.0f;
				translateAnimScript.endPos = pos;
				translateAnimScript.easingType = EasingType.EaseInSine;

				translateAnimScript.InitMe();
				translateAnimScript.IsUpdating = true;

				translateAnimScript = translateAnimScripts[3];

				translateAnimScript.animDuration = 0.5f;
				translateAnimScript.startTimeOffset = 1.5f;
				translateAnimScript.periodicDelay = 1.5f;
				translateAnimScript.countThreshold = 0;

				translateAnimScript.myTransform = gameUnitAttribs.markerGO.transform;
				translateAnimScript.startPos = pos;
				pos.y -= 2.0f;
				translateAnimScript.endPos = pos;
				translateAnimScript.easingType = EasingType.EaseOutSine;

				translateAnimScript.InitMe();
				translateAnimScript.IsUpdating = true;

				rotateAnimScript = gameUnitAttribs.markerGO.GetComponent<TransformRotateAnim>();

				rotateAnimScript.animDuration = 0.5f;
				rotateAnimScript.startTimeOffset = 0.0f;
				rotateAnimScript.periodicDelay = 0.5f;
				rotateAnimScript.countThreshold = 0;

				rotateAnimScript.myTransform = gameUnitAttribs.markerGO.transform;
				rotateAnimScript.startEulerAngles.x = 180.0f;
				rotateAnimScript.endEulerAngles.x = 180.0f;
				rotateAnimScript.endEulerAngles.y = 360.0f;
				rotateAnimScript.easingType = EasingType.EaseOutCubic;

				rotateAnimScript.InitMe();
				rotateAnimScript.IsUpdating = true;
			}
		}
    }
}