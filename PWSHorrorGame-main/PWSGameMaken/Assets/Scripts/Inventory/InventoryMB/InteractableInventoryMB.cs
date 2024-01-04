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

		[SerializeField] private VisibleSlotsMB _visibleSlotsMB;

		private HintTextMB _hintTextMB;
		private GameObject _selectedGO;
		#endregion

		private void Start()
		{
			_hintTextMB = HintTextMB.instance;
		}

		private void Update()
		{
			RaycastHit raycastHit = MakeRaycast(_interactionFirePoint.position, _interactionFirePoint.forward, _interactionDistanceLimit);
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
			else if (Input.GetKeyUp(KeyCode.E)) UnInteract();
			else if (Input.GetKeyDown(KeyCode.Q)) DropItems();
		}

		private void DropItems()
		{
			_visibleSlotsMB.DropItems(_visibleSlotsMB.selectedSlot);
		}

		private void Interact(GameObject itemToInteract)
		{
			_selectedGO = itemToInteract;

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

		private void UnInteract()
		{
			if (_selectedGO != null && _selectedGO.TryGetComponent(out RotatableMirrorMB rotatableMirror))
			{
				rotatableMirror.Interact();
			}
		}
	}
}