#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using static FurnishAR.Anim.AnimAccessTypes;

namespace FurnishAR.Anim {
	internal abstract class AbstractAnimEditor: Editor {
		#region Fields

		protected AbstractAnim myScript;

		protected string[] names;
		private uint namesLen;

		private SerializedProperty[] serializedProperties;

		private GUIContent[] GUIContents;

		#endregion

		#region Properties
		#endregion

		#region Ctors and Dtor

		internal AbstractAnimEditor(): base() {
			myScript = null;

			names = System.Array.Empty<string>();
			namesLen = 0u;

			serializedProperties = System.Array.Empty<SerializedProperty>();

			GUIContents = System.Array.Empty<GUIContent>();
		}

		static AbstractAnimEditor() {
		}

		#endregion

		#region Unity User Callback Event Funcs
		#endregion

		protected abstract void InitNames();

		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			myScript = target as AbstractAnim;

			InitNames();

			namesLen = (uint)names.Length;
			serializedProperties = new SerializedProperty[namesLen];
			GUIContents = new GUIContent[namesLen];

			for(uint i = 0; i < namesLen; ++i) {
				serializedProperties[i] = serializedObject.FindProperty(names[i]);
				GUIContents[i] = new GUIContent(names[i]);
			}

			if(PrefabUtility.IsPartOfAnyPrefab(myScript)) {
				PrefabUtility.RecordPrefabInstancePropertyModifications(myScript);
			}

			switch(myScript.AccessType) {
				case AnimAccessType.Editor:
					serializedObject.UpdateIfRequiredOrScript();

					for(uint i = 0; i < namesLen; ++i) {
						if(myScript.countThreshold == 1 && names[i] == "periodicDelay") {
							continue;
						}

						_ = EditorGUILayout.PropertyField(serializedProperties[i], GUIContents[i]);
					}

					_ = serializedObject.ApplyModifiedProperties();

					break;
				case AnimAccessType.Script:
					break;
				case AnimAccessType.Amt:
					if(GUI.changed) {
						Generic.Console.LogError("myScript.AccessType == AnimAccessType.Amt");
					}

					break;
			}
		}
	}
}

#endif