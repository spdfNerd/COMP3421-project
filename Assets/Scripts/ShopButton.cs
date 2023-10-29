using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	private Image image;
	private Button button;

	private bool isSelected = false;

	private void Start() {
		image = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	public void ToggleSelect() {
		isSelected = !isSelected;
		image.color = isSelected ? button.colors.selectedColor : button.colors.normalColor;
	}

}
