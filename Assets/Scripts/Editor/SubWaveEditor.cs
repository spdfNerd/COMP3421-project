using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Wave.SubWave))]
public class SubWaveEditor : InlinePropertyDrawer {

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		SetPropertyNames("customer", "count", "spawnRate");
		SetFoldout(false);
		return base.GetPropertyHeight(property, label);
	}

}
