using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor {

	private Quest quest;

	private void OnEnable() {
		quest = (Quest) target;
	}

	public override void OnInspectorGUI() {
		using (new EditorGUI.DisabledScope(true)) {
			EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
		}
		DrawDetailsFields();
		DrawRequirementFields();
		DrawRewardsFields();
		DrawGraphicsFields();
		serializedObject.ApplyModifiedProperties();

		if (quest.questNameText != null) {
			quest.questNameText.text = quest.questName;
		}
		if (quest.rewardsText != null) {
			quest.rewardsText.text = quest.GetRewardsText();
		}
	}

	private void DrawDetailsFields() {
		EditorGUILayout.PropertyField(serializedObject.FindProperty("questName"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("questType"));
	}

	private void DrawRequirementFields() {
		EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredAmount"));
		switch (quest.questType) {
			case QuestType.SERVE_FOOD:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("foodType"));
				break;
			case QuestType.SERVE_CUSTOMER:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("customerType"));
				break;
			default:
				break;
		}
	}

	private void DrawRewardsFields() {
		switch (quest.questType) {
			case QuestType.SERVE_FOOD:
			case QuestType.SERVE_CUSTOMER:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("cashReward"));
				break;
			case QuestType.SPEND:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("reputationReward"));
				break;
		}
	}

	private void DrawGraphicsFields() {
		EditorGUILayout.PropertyField(serializedObject.FindProperty("questNameText"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("rewardsText"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("progressText"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("progressBarBackground"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("progressBar"));
	}

}
