using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StaffCosts))]
public class StaffCostsEditor : InlinePropertyDrawer {

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		SetPropertyNames("hirePrice", "sellPrice", "runningCost");
		SetFoldout(false);
		return base.GetPropertyHeight(property, label);
	}

}
