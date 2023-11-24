using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	private Image image;
	private Button button;

	private void Start() {
		image = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	public void SetSelected(bool selected) {
		image.color = selected ? button.colors.selectedColor : button.colors.normalColor;
	}

}
