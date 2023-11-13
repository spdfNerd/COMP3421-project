using Unity.VisualScripting;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor {

	private LevelManager levelManager;

	private void OnEnable() {
		levelManager = target.GetComponent<LevelManager>();
	}

	public override void OnInspectorGUI() {
		if (levelManager.isEndlessMode) {
			base.OnInspectorGUI();
		} else {
			EditorGUILayout.LabelField("This level is not in endless mode, no quests will be used!");
		}
	}

}
