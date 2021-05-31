using UnityEngine;

namespace FurnishAR.Generic {
	internal sealed partial class Instancer: MonoBehaviour {
		public static void InstancingWithColor() {
			//* Pos 
			globalObj.posComputeBuffer = new ComputeBuffer((int)globalObj.instanceCount, 16);

			int posKernelIndex = globalObj.computeShader.FindKernel("PosMain");
			globalObj.computeShader.SetBuffer(posKernelIndex, "posRWStructuredBuffer", globalObj.posComputeBuffer);
			globalObj.computeShader.Dispatch(posKernelIndex, 1024, 1, 1);

			globalObj.instanceMtl.SetBuffer("posStructuredBuffer", globalObj.posComputeBuffer);
			//*/

			//* Colors
			globalObj.colorComputeBuffer = new ComputeBuffer((int)globalObj.instanceCount, 12);

			int colorKernelIndex = globalObj.computeShader.FindKernel("ColorMain");
			globalObj.computeShader.SetBuffer(colorKernelIndex, "colorRWStructuredBuffer", globalObj.colorComputeBuffer);
			globalObj.computeShader.Dispatch(colorKernelIndex, 1024, 1, 1);

			globalObj.instanceMtl.SetBuffer("colorStructuredBuffer", globalObj.colorComputeBuffer);
			//*/
		}
	}
}