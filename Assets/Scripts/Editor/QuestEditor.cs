using UnityEditor;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor {

	private Quest quest;

	public override void OnInspectorGUI() {
		quest = (Quest) target;
		DrawDetailsFields();
		DrawRequirementFields();
		DrawRewardsFields();
	}

	private void DrawDetailsFields() {
		quest.questName = EditorGUILayout.TextField("Quest Name", quest.questName);
		quest.questType = (QuestType) EditorGUILayout.EnumPopup("Quest Type", quest.questType);
	}

	private void DrawRequirementFields() {
		quest.requiredAmount = EditorGUILayout.IntField("Required Amount", quest.requiredAmount);
		switch (quest.questType) {
			case QuestType.SERVE_FOOD:
				quest.foodType = (FoodType) EditorGUILayout.EnumPopup("Food Type", quest.foodType);
				break;
			case QuestType.SERVE_CUSTOMER:
				quest.customerType = (CustomerType) EditorGUILayout.EnumPopup("Customer Type", quest.customerType);
				break;
			default:
				break;
		}
	}

	private void DrawRewardsFields() {
		switch (quest.questType) {
			case QuestType.SERVE_FOOD:
			case QuestType.SERVE_CUSTOMER:
				quest.cashReward = EditorGUILayout.IntField("Cash Reward", quest.cashReward);
				break;
			case QuestType.SPEND:
				quest.reputationReward = EditorGUILayout.IntField("Reputation Reward", quest.reputationReward);
				break;
		}
	}

}
