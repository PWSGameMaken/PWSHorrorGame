/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

public interface IInteractWithSlot
{
	void Interact(VisibleSlotsMB visibleSlotsMB);
}

public interface IInteractWithoutSlot
{
	void Interact();
}

namespace Inventory
{
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		[SerializeField] private Transform _interactionFirePoint;
		[SerializeField] private float _interactionDistanceLimit;
		[SerializeField] private HintTextMB _hintTextMB;
		[SerializeField] private VisibleSlotsMB _visibleSlotsMB;
		private GameObject _lastSelectedGO;
		#endregion

		private void Update()
		{
			RaycastHit raycastHit = MakeRaycast(_interactionFirePoint.position, _interactionFirePoint.transform.forward, _interactionDistanceLimit);
			var collidedGO = raycastHit.transform?.gameObject;

			ScrollSlots();

			_hintTextMB.UpdateHintUI(collidedGO);

			CheckInputs(collidedGO);
		}

		private RaycastHit MakeRaycast(Vector3 originPos, Vector3 direction, float maxDistance)
		{
			Physics.Raycast(originPos, direction, out RaycastHit hit, maxDistance);
			Debug.DrawRay(originPos, direction * hit.distance, Color.yellow);

			return hit;
		}

		private void ScrollSlots()
		{
			var scrollDelta = (int)Input.mouseScrollDelta.y;

			if (scrollDelta != 0)
			{
				_visibleSlotsMB.ChangeSelectedSlot(scrollDelta);
			}
		}

		private void CheckInputs(GameObject collidedGO)
		{
			if (Input.GetKeyDown(KeyCode.E)) Interact(collidedGO);
			else if (Input.GetKeyUp(KeyCode.E)) UnInteract(_lastSelectedGO);
			else if (Input.GetKeyDown(KeyCode.Q)) _visibleSlotsMB.DropItems();
		}

		private void Interact(GameObject itemToInteract)
		{
			_lastSelectedGO = itemToInteract;

			if (itemToInteract != null && itemToInteract.TryGetComponent(out InteractableObjectMB interactableObjectMB))
			{
				if (interactableObjectMB is IInteractWithSlot withSlot)
				{
					Interact(withSlot);
				}
				else if (interactableObjectMB is IInteractWithoutSlot withoutSlot)
				{
					Interact(withoutSlot);
				}
			}
		}

		private void Interact(IInteractWithSlot interactableObjectMB)
		{
			interactableObjectMB.Interact(_visibleSlotsMB);
		}

		private void Interact(IInteractWithoutSlot interactableObjectMB)
		{
			interactableObjectMB.Interact();
		}

		private void UnInteract(GameObject itemToUnInteract)
		{
			if (itemToUnInteract != null && itemToUnInteract.TryGetComponent(out RotatableMirrorMB rotatableMirror))
			{
				rotatableMirror.Interact();
			}
		}
	}
}