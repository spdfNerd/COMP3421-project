using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Wave))]
public class WaveEditor : InlinePropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		SetPropertyNames("subWaves");
		SetFoldout(true, "Wave");
		base.OnGUI(position, property, label);
	}

}
