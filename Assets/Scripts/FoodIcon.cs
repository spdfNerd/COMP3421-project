using UnityEngine;
using UnityEngine.EventSystems;

public class FoodIcon : MonoBehaviour, IPointerDownHandler {

    public FoodType type;
    public InventoryScreen inventoryScreen;

	public void OnPointerDown(PointerEventData eventData) {
		inventoryScreen.UpdateSelectedFood((int) type);
	}

}
