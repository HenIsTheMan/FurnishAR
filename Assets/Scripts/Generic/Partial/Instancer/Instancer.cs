using UnityEngine;
using static IWP.Generic.InitIDs;

namespace IWP.Generic {
    internal sealed partial class Instancer: MonoBehaviour {
		#region Fields

		[SerializeField]
		private InitControl initControl;

		private uint[] args;
		private Bounds bounds;
		private ComputeBuffer posComputeBuffer;
		private ComputeBuffer colorComputeBuffer;
		private ComputeBuffer argComputeBuffer;

		[SerializeField]
		private string instancingMethodName;

		[SerializeField]
		private float sizeFactor;

		[SerializeField]
		private ComputeShader computeShader;

		[SerializeField]
		private int subMeshIndex;

		[SerializeField]
		private uint instanceCount;

		[SerializeField]
		private Mesh instanceMesh;

		[SerializeField]
		private Material instanceMtl;

		private static Instancer globalObj;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal Instancer(): base() {
			initControl = null;

			args = System.Array.Empty<uint>();
			//bounds;
			posComputeBuffer = null;
			colorComputeBuffer = null;
			argComputeBuffer = null;

			instancingMethodName = string.Empty;
			sizeFactor = 0.0f;
			computeShader = null;
			subMeshIndex = 0;
			instanceCount = 0u;
			instanceMesh = null;
			instanceMtl = null;
		}

        static Instancer() {
			globalObj = null;
        }

        #endregion

        #region Unity User Callback Event Funcs

        private void OnValidate() {
			if(instanceMesh != null) {
				subMeshIndex = Mathf.Clamp(subMeshIndex, 0, instanceMesh.subMeshCount - 1);
			}
        }

		private void OnEnable() {
			initControl.AddMethod((uint)InitID.Instancer, Init);
		}

		private void Update() {
			Graphics.DrawMeshInstancedIndirect(instanceMesh, subMeshIndex, instanceMtl, bounds, argComputeBuffer);
		}

		private void OnDisable() {
			initControl.RemoveMethod((uint)InitID.Instancer, Init);

			if(posComputeBuffer != null) {
				posComputeBuffer.Dispose();
			}

			if(colorComputeBuffer != null) {
				colorComputeBuffer.Dispose();
			}

			if(argComputeBuffer != null) {
				argComputeBuffer.Release();
			}
		}

		#endregion

		private void Init() {
			globalObj = this;

			args = new uint[5]{
				instanceMesh.GetIndexCount(subMeshIndex),
				instanceCount,
				instanceMesh.GetIndexStart(subMeshIndex),
				instanceMesh.GetBaseVertex(subMeshIndex),
				0u
			};

			argComputeBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
			argComputeBuffer.SetData(args);

			bounds = new Bounds(Vector3.zero, Vector3.one * sizeFactor);

			if(instancingMethodName != string.Empty) {
				_ = GetType().GetMethod(instancingMethodName).Invoke(this, null);
			}
		}
	}
}