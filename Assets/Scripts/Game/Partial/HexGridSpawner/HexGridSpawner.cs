using FurnishAR.Anim;
using FurnishAR.Generic;
using UnityEngine;
using static FurnishAR.Game.HexGridCellTypes;
using static FurnishAR.Game.HexGridTypes;
using static FurnishAR.Math.EasingTypes;

namespace FurnishAR.Game {
	internal sealed partial class HexGridSpawner: MonoBehaviour {
		#region Fields

		[SerializeField]
		private HexGridManager hexGridManager;

		[SerializeField]
		private GameObject hexPrefab;

		[SerializeField]
		private Transform parentTransform;

		[SerializeField]
		private ObjPool hexPool;

		[SerializeField]
		private int size;

		[SerializeField]
		private float minStartZ;

		[SerializeField]
		private float maxStartZ;

		[SerializeField]
		private float endZ;

		[SerializeField]
		private EasingType fadeEasingType;

		[SerializeField]
		private EasingType translateEasingType;

		[SerializeField]
		private float fadeStartOffset;

		[SerializeField]
		private float translateStartOffset;

		[Min(0.0f), SerializeField]
		private float minFadeAnimDuration;

		[SerializeField]
		private float maxFadeAnimDuration;

		[Min(0.0f), SerializeField]
		private float minTranslateAnimDuration;

		[SerializeField]
		private float maxTranslateAnimDuration;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal HexGridSpawner(): base() {
			hexGridManager = null;

			hexPrefab = null;
			parentTransform = null;

			hexPool = null;
			size = 0;

			minStartZ = 0.0f;
			maxStartZ = 0.0f;
			endZ = 0.0f;
			fadeEasingType = EasingType.Amt;
			translateEasingType = EasingType.Amt;
			fadeStartOffset = 0.0f;
			translateStartOffset = 0.0f;
			minFadeAnimDuration = 0.0f;
			maxFadeAnimDuration = 0.0f;
			minTranslateAnimDuration = 0.0f;
			maxTranslateAnimDuration = 0.0f;
		}

        static HexGridSpawner() {
        }

		#endregion

		#region Unity User Callback Event Funcs

		private void OnValidate() {
			UnityEngine.Assertions.Assert.IsTrue(minStartZ <= maxStartZ, "minStartZ <= maxStartZ");

			UnityEngine.Assertions.Assert.IsTrue(
				minFadeAnimDuration <= maxFadeAnimDuration,
				"minFadeAnimDuration <= maxFadeAnimDuration"
			);
			UnityEngine.Assertions.Assert.IsTrue(
				minTranslateAnimDuration <= maxTranslateAnimDuration,
				"minTranslateAnimDuration <= maxTranslateAnimDuration"
			);
		}

		#endregion

		internal void InitMe() {
			hexPool.InitMe(size, hexPrefab, parentTransform, false);
		}

		internal void Spawn(string spawnMethodName) {
			if(spawnMethodName != string.Empty) {
				_ = GetType().GetMethod(spawnMethodName).Invoke(this, null);
			}
		}

		private void SpawnHexGrid() {
			HexGridType gridType = hexGridManager.type;
			float gridCellScaleX = hexGridManager.cellScaleX;
			float gridCellScaleY = hexGridManager.cellScaleY;
			float gridCellScaleZ = hexGridManager.cellScaleZ;
			float gridLineThickness = hexGridManager.lineThickness;
			int gridRows = hexGridManager.rows;
			int gridCols = hexGridManager.cols;

			float gridCellSharpToSharpLen = hexGridManager.CalcCellSharpToSharpLen();
			float gridCellFlatToFlatLen = hexGridManager.CalcCellFlatToFlatLen();
			float gridCellSideLen = hexGridManager.CalcCellSideLen();
			float gridAltOffset = hexGridManager.CalcAltOffset();

			float gridOffsetX = hexGridManager.CalcOffsetX();
			float gridOffsetY = hexGridManager.CalcOffsetY();

			GameObject gridCellGO;
			HexGridCellAttribs gridCellAttribs;

			GameObject gridCellImgGO;
			MtlFadeAnim fadeAnimScript;
			TransformScaleAnim scaleAnimScript;
			TransformShakeAnim shakeAnimScript;
			TransformTranslateAnim translateAnimScript;

			Vector3 startPos;
			Vector3 endPos;
			Vector3 translate;
			Quaternion rotate;
			Vector3 scale;

			for(int r = 0; r < gridRows; ++r) {
				for(int c = 0; c < gridCols; ++c) {
					gridCellGO = hexPool.ActivateObj();
					gridCellGO.name = hexPrefab.name + (r * gridCols + c).ToString();
					gridCellGO.GetComponent<OutlineSilhouette>().InitMe();

					hexGridManager.gridCellGOs[r * gridCols + c] = gridCellGO;

					gridCellAttribs = gridCellGO.GetComponent<HexGridCellAttribs>();

					AssignHexGridCellType(gridCellAttribs);

					gridCellImgGO = gridCellAttribs.meshRenderer.gameObject;

					fadeAnimScript = gridCellImgGO.GetComponent<MtlFadeAnim>();
					fadeAnimScript.startTimeOffset = 3.0f;
					fadeAnimScript.mtl = gridCellImgGO.GetComponent<MeshRenderer>().material;
					fadeAnimScript.InitMe();
					fadeAnimScript.IsUpdating = true;

					scaleAnimScript = gridCellImgGO.GetComponent<TransformScaleAnim>();
					scaleAnimScript.startTimeOffset = 4.0f;
					scaleAnimScript.InitMe();
					scaleAnimScript.IsUpdating = true;

					shakeAnimScript = gridCellImgGO.GetComponent<TransformShakeAnim>();
					shakeAnimScript.startTimeOffset = 4.0f;
					shakeAnimScript.InitMe();
					shakeAnimScript.IsUpdating = true;

					fadeAnimScript = gridCellGO.GetComponent<MtlFadeAnim>();

					fadeAnimScript.animDuration = Random.Range(minFadeAnimDuration, maxFadeAnimDuration);
					fadeAnimScript.startTimeOffset = fadeStartOffset;
					//fadeAnimScript.periodicDelay = 0.0f;
					fadeAnimScript.countThreshold = 1;

					fadeAnimScript.mtl = gridCellGO.GetComponent<Renderer>().material;
					fadeAnimScript.startAlpha = 0.0f;
					fadeAnimScript.endAlpha = 1.0f;
					fadeAnimScript.easingType = fadeEasingType;

					fadeAnimScript.InitMe();
					fadeAnimScript.IsUpdating = true;

					translateAnimScript = gridCellGO.GetComponent<TransformTranslateAnim>();

					if(gridType == HexGridType.FlatTop) {
						translate.x = gridOffsetX + (gridCellSharpToSharpLen * 0.5f + gridCellSideLen * 0.5f + gridLineThickness) * c;
						translate.y = gridOffsetY + (gridCellFlatToFlatLen + gridLineThickness) * r + (c & 1) * gridAltOffset;
					} else {
						translate.x = gridOffsetX + (gridCellFlatToFlatLen + gridLineThickness) * c + (r & 1) * gridAltOffset;
						translate.y = gridOffsetY + (gridCellSharpToSharpLen * 0.5f + gridCellSideLen * 0.5f + gridLineThickness) * r;

						rotate = Quaternion.Euler(0.0f, 0.0f, 90.0f);
						gridCellGO.transform.localRotation = rotate;
					}

					scale.x = gridCellScaleX;
					scale.y = gridCellScaleY;
					scale.z = gridCellScaleZ;

					translate.z = endZ;
					startPos = endPos = translate;
					startPos.z = Random.Range(minStartZ, maxStartZ);

					translateAnimScript.animDuration = Random.Range(minTranslateAnimDuration, maxTranslateAnimDuration);
					translateAnimScript.startTimeOffset = translateStartOffset;
					//translateAnimScript.periodicDelay = 0.0f;
					translateAnimScript.countThreshold = 1;

					translateAnimScript.myTransform = gridCellGO.transform;
					translateAnimScript.startPos = startPos;
					translateAnimScript.endPos = endPos;
					translateAnimScript.easingType = translateEasingType;

					translateAnimScript.InitMe();
					translateAnimScript.IsUpdating = true;

					gridCellGO.transform.localScale = scale;
				}
			}
		}

		private void AssignHexGridCellType(HexGridCellAttribs gridCellAttribs) {
			int sumOfCellWeights = 0;
			uint cellWeightsLen = (uint)HexGridCellType.Amt;

			for(uint i = 0; i < cellWeightsLen; ++i) {
				sumOfCellWeights += hexGridManager.cellWeights[i];
			}
			if(sumOfCellWeights == 0) {
				Console.LogError("sumOfCellWeights == 0");
				return;
			}

			int val = Random.Range(1, sumOfCellWeights + 1);
			int cellWeight;

			for(uint i = 0; i < cellWeightsLen; ++i) {
				cellWeight = hexGridManager.cellWeights[i];

				if(val <= cellWeight) {
					gridCellAttribs.Type = (HexGridCellType)i;
					break;
				}

				val -= cellWeight;
			}
		}
	}
}