using IWP.Anim;
using TMPro;
using UnityEngine;

namespace IWP.Generic {
    internal sealed partial class TextPtrOverLineOfWords: MonoBehaviour {
		public static void PtrNotOverInstructionText() {
			GameObject instructionText = globalObj.tmpTextComponent.gameObject;
			RectTransformScaleAnim[] scaleAnimScripts = instructionText.GetComponents<RectTransformScaleAnim>();

			scaleAnimScripts[0].IsUpdating = true;
			scaleAnimScripts[1].IsUpdating = true;
			instructionText.GetComponent<TextMeshProUGUI>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}
}