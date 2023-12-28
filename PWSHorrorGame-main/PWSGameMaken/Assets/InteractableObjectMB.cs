using UnityEngine;

public abstract class InteractableObjectMB : MonoBehaviour
{
	public abstract void Interact(GameObject itemToInteract, VisibleSlotsMB visibleSlotsMB);
	public abstract string GetHintUIText();
}
