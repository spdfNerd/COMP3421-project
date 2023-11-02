using UnityEditor;
using UnityEngine;

public class InlinePropertyDrawer : PropertyDrawer {

	private string[] propertyNames;
	private bool isFoldout;
	private string foldoutName;

	public void SetPropertyNames(params string[] names) {
		propertyNames = names;
	}

	public void SetFoldout(bool isFoldout, string foldoutName = "") {
		this.isFoldout = isFoldout;
		this.foldoutName = foldoutName;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return EditorGUIUtility.singleLineHeight * (property.isExpanded ? propertyNames.Length : 2);
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginProperty(position, label, property);
		
		if (isFoldout) {
			property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, property.isExpanded, foldoutName);
			if (property.isExpanded) {
				EditorGUI.indentLevel++;
				DrawElements(position, property);
				EditorGUI.indentLevel--;
			}
			EditorGUI.EndFoldoutHeaderGroup();
		} else {
			DrawElements(position, property);
		}
		
		EditorGUI.EndProperty();

		if (EditorGUI.EndChangeCheck()) {
			EditorUtility.SetDirty(property.serializedObject.targetObject);
		}
	}

	private void DrawElements(Rect position, SerializedProperty property) {
		SerializedProperty[] properties = new SerializedProperty[propertyNames.Length];
		for (int i = 0; i < properties.Length; i++) {
			properties[i] = property.FindPropertyRelative(propertyNames[i]);
		}

		float fieldWidth = (EditorGUIUtility.currentViewWidth - 1) / properties.Length - (isFoldout ? 0 : 4);
		Rect baseRect = position;
		baseRect.width = fieldWidth;
		baseRect.height = EditorGUIUtility.singleLineHeight;

		Rect fieldLabelPos = baseRect;
		Rect fieldPos = baseRect;
		
		if (isFoldout) {
			fieldLabelPos.y += EditorGUIUtility.singleLineHeight;
			fieldPos.y += 2 * EditorGUIUtility.singleLineHeight;
		} else {
			fieldPos.y += EditorGUIUtility.singleLineHeight;
		}

		for (int i = 0; i < properties.Length; i++) {
			fieldLabelPos.x = i * (fieldWidth - (isFoldout ? 2 : 0));
			EditorGUI.LabelField(fieldLabelPos, properties[i].displayName);
			fieldPos.x = i * (fieldWidth - (isFoldout ? 2 : -5));
			EditorGUI.PropertyField(fieldPos, properties[i], GUIContent.none);
		}
	}

}
