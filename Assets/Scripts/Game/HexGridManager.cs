using IWP.Generic;
using UnityEngine;
using static IWP.Game.GameUnitTypeFlags;
using static IWP.Game.HexGridCellTypes;
using static IWP.Game.HexGridHorizAlignments;
using static IWP.Game.HexGridTypes;
using static IWP.Game.HexGridVertAlignments;

namespace IWP.Game {
    internal sealed class HexGridManager: MonoBehaviour {
		#region Fields

		internal GameObject[] gridCellGOs;

		internal GameObject srcGridCellGO;
		internal GameObject dstGridCellGO;

		internal int[] cellWeights;

		private HexGridCellAttribs hexGridCellAttribs;
		private OutlineSilhouette outlineSilhouette;
		private Transform hitInfoTransform;

		[SerializeField]
		internal Texture2D[] cellTexs;

		[SerializeField]
		internal HexGridType type;

		[SerializeField]
		internal HexGridHorizAlignment horizAlignment;

		[SerializeField]
		internal HexGridVertAlignment vertAlignment;

		[Min(0.0f), SerializeField]
		internal float cellScaleX;

		[Min(0.0f), SerializeField]
		internal float cellScaleY;

		[Min(0.0f), SerializeField]
		internal float cellScaleZ;

		[Min(0.0f), SerializeField]
		internal float lineThickness;

		[Min(0), SerializeField]
		internal int rows;

		[Min(0), SerializeField]
		internal int cols;

		private Ray ray;

		[SerializeField]
		private Camera cam;

		internal static HexGridManager globalObj;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal HexGridManager(): base() {
			gridCellGOs = System.Array.Empty<GameObject>();

			srcGridCellGO = null;
			dstGridCellGO = null;

			cellWeights = System.Array.Empty<int>();

			hexGridCellAttribs = null;
			outlineSilhouette = null;
			hitInfoTransform = null;

			type = HexGridType.Amt;
			horizAlignment = HexGridHorizAlignment.Amt;
			vertAlignment = HexGridVertAlignment.Amt;

			cellScaleX = 1.0f;
			cellScaleY = 1.0f;
			cellScaleZ = 1.0f;
			lineThickness = 0.0f;

			rows = 0;
			cols = 0;

			//ray;
			cam = null;
        }

        static HexGridManager() {
		}

		#endregion

		#region Unity User Callback Event Funcs

		private void OnValidate() {
			UnityEngine.Assertions.Assert.AreEqual(
				cellTexs.Length, (int)HexGridCellType.Amt,
				"cellTexs.Length, (int)HexGridCellType.Amt"
			);

			UnityEngine.Assertions.Assert.AreNotEqual(type, HexGridType.Amt, "type, HexGridType.Amt");
			UnityEngine.Assertions.Assert.AreNotEqual(
				horizAlignment, HexGridHorizAlignment.Amt,
				"horizAlignment, HexGridHorizAlignment.Amt"
			);
			UnityEngine.Assertions.Assert.AreNotEqual(
				vertAlignment, HexGridVertAlignment.Amt,
				"vertAlignment, HexGridVertAlignment.Amt"
			);
		}

		private void Update() {
			if(Input.GetMouseButton(0)) {
				ray = cam.ScreenPointToRay(Input.mousePosition);

				if(Physics.Raycast(ray, out RaycastHit hitInfo)) {
					hitInfoTransform = hitInfo.transform;

					if(hitInfoTransform.parent.name != "HexGrid") {
						return;
					}

					if(hitInfoTransform.gameObject != srcGridCellGO) {
						if(srcGridCellGO != null) {
							if(dstGridCellGO != null) {
								dstGridCellGO.GetComponent<OutlineSilhouette>().OutlineSilhouetteColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							}

							hexGridCellAttribs = hitInfoTransform.GetComponent<HexGridCellAttribs>();

							if(hexGridCellAttribs.gameUnitOnMe != null
								&& ((int)hexGridCellAttribs.gameUnitOnMe.GetComponent<GameUnitAttribs>().gameUnitTypes & (int)GameUnitTypes.MyTeam) > 0) {
								srcGridCellGO.GetComponent<OutlineSilhouette>().OutlineSilhouetteColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

								srcGridCellGO = hitInfoTransform.gameObject;

								outlineSilhouette = srcGridCellGO.GetComponent<OutlineSilhouette>();

								outlineSilhouette.OutlineSilhouetteColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							} else {
								dstGridCellGO = hitInfoTransform.gameObject;

								outlineSilhouette = dstGridCellGO.GetComponent<OutlineSilhouette>();

								outlineSilhouette.OutlineSilhouetteColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
							}
						} else {
							hexGridCellAttribs = hitInfoTransform.GetComponent<HexGridCellAttribs>();

							if(hexGridCellAttribs.gameUnitOnMe != null
								&& ((int)hexGridCellAttribs.gameUnitOnMe.GetComponent<GameUnitAttribs>().gameUnitTypes & (int)GameUnitTypes.MyTeam) > 0) {
								srcGridCellGO = hitInfoTransform.gameObject;

								outlineSilhouette = srcGridCellGO.GetComponent<OutlineSilhouette>();

								outlineSilhouette.OutlineSilhouetteColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							}
						}
					}
				}
			}
		}

		#endregion

		internal void InitMe() {
			globalObj = this;

			int size = rows * cols;
			gridCellGOs = new GameObject[size];

			cellWeights = new int[(uint)HexGridCellType.Amt];
		}

		internal int CalcAmtOfHorizLines() {
			return rows + 1;
		}

		internal int CalcAmtOfVertLines() {
			return cols + 1;
		}

		internal float CalcWidth() {
			return type == HexGridType.FlatTop
				? (cols - 1) * (CalcCellSharpToSharpLen() * 0.5f + CalcCellSideLen() * 0.5f + lineThickness)
				: (cols - 1) * (CalcCellFlatToFlatLen() + lineThickness) + CalcCellFlatToFlatLen() * 0.5f;
		}

		internal float CalcHeight() {
			return type == HexGridType.FlatTop
				? (rows - 1) * (CalcCellFlatToFlatLen() + lineThickness) + CalcCellFlatToFlatLen() * 0.5f
				: (rows - 1) * (CalcCellSharpToSharpLen() * 0.5f + CalcCellSideLen() * 0.5f + lineThickness);
		}

		internal float CalcCellSharpToSharpLen() {
			return type == HexGridType.FlatTop
				? cellScaleX
				: cellScaleY;
		}

		internal float CalcCellFlatToFlatLen() {
			return (type == HexGridType.FlatTop
				? cellScaleX
				: cellScaleY) * Mathf.Sin(60.0f * Mathf.Deg2Rad);
		}

		internal float CalcCellSideLen() {
			return (type == HexGridType.FlatTop
				? cellScaleX
				: cellScaleY) * Mathf.Cos(60.0f * Mathf.Deg2Rad);
		}

		internal float CalcAltOffset() {
			return (CalcCellFlatToFlatLen() + lineThickness) * 0.5f;
		}

		internal float CalcOffsetX() {
			switch(horizAlignment) {
				case HexGridHorizAlignment.Left:
					return 0.0f;
				case HexGridHorizAlignment.Center:
					return -CalcWidth() * 0.5f;
				case HexGridHorizAlignment.Right:
					return -CalcWidth();
			}

			throw new System.Exception();
		}
		internal float CalcOffsetY() {
			switch(vertAlignment) {
				case HexGridVertAlignment.Top:
					return -CalcHeight();
				case HexGridVertAlignment.Middle:
					return -CalcHeight() * 0.5f;
				case HexGridVertAlignment.Bottom:
					return 0.0f;
			}

			throw new System.Exception();
		}
	}
}