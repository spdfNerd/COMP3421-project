using UnityEditor;
using UnityEngine;

public class InlinePropertyDrawer : PropertyDrawer {

	private string[] propertyNames;
	private bool isFoldout;
	private string foldoutName;

	public void SetPropertyNames(params string[] names) {
		propertyNames = names;
	}

	/// <summary>
	/// Set whether drawer is a fold out drawer
	/// </summary>
	/// <param name="isFoldout">Whether drawer is a fold out drawer</param>
	/// <param name="foldoutName">Name of fold out drawer</param>
	public void SetFoldout(bool isFoldout, string foldoutName = "") {
		this.isFoldout = isFoldout;
		this.foldoutName = foldoutName;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		// Minimum amount of lines = 2
		int lines = (isFoldout && property.isExpanded) ? propertyNames.Length : 2;
		return EditorGUIUtility.singleLineHeight * lines;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Make sure the object values change to reflect what is in the editor
		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginProperty(position, label, property);
		
		if (isFoldout) {
			property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(position, property.isExpanded, foldoutName);
			if (property.isExpanded) {
				// Make sure the elements are indented to represent that they are part of the fold out
				EditorGUI.indentLevel++;
				DrawElements(position, property);
				EditorGUI.indentLevel--;
			}
			EditorGUI.EndFoldoutHeaderGroup();
		} else {
			// Can just draw elements without indenting if not folded out
			DrawElements(position, property);
		}
		
		EditorGUI.EndProperty();
		if (EditorGUI.EndChangeCheck()) {
			// Flags the editor to update the object values
			EditorUtility.SetDirty(property.serializedObject.targetObject);
		}
	}

	/// <summary>
	/// Draw all elements that should be shown
	/// </summary>
	/// <param name="position">Position of the parent property</param>
	/// <param name="property">Parent property to be drawn</param>
	private void DrawElements(Rect position, SerializedProperty property) {
		// Get all child properties
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
		
		// Adjust label and field positions according to state of fold out
		if (isFoldout) {
			fieldLabelPos.y += EditorGUIUtility.singleLineHeight;
			fieldPos.y += 2 * EditorGUIUtility.singleLineHeight;
		} else {
			fieldPos.y += EditorGUIUtility.singleLineHeight;
		}

		for (int i = 0; i < properties.Length; i++) {
			// Draw label field for property
			fieldLabelPos.x = i * (fieldWidth - (isFoldout ? 2 : -5));
			EditorGUI.LabelField(fieldLabelPos, properties[i].displayName);
			// Draw property field
			fieldPos.x = i * (fieldWidth - (isFoldout ? 2 : -5));
			EditorGUI.PropertyField(fieldPos, properties[i], GUIContent.none);
		}
	}

}
