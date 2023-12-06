/*
* Grobros
* https://github.com/GroBro-s
*/

using System.Collections;
using TMPro;
using UnityEngine;

namespace Inventory
{
	public class InteractableInventoryMB : ParentInventoryMB
	{
		#region variables
		[Header("Interaction With Items")]
		[SerializeField] private Transform _firePoint;
		[SerializeField] private float _interactionDistanceLimit;
		[SerializeField] private TextMeshProUGUI hintText;
		[SerializeField] private GameObject _lastSelectedGO;
		[SerializeField] private VisibleSlotsMB _visibleSlotsMB;

		private bool _isMoving = false;
		#endregion

		private void Update()
		{
			RaycastHit raycastHit = MakeRaycast(_firePoint.position, _firePoint.transform.forward, _interactionDistanceLimit);
			var collidedGO = raycastHit.transform?.gameObject;

			ScrollSlots();

			UpdateHintUI(collidedGO);


			if (Input.GetKeyDown(KeyCode.E)) Interact(collidedGO);
			else if (Input.GetKeyUp(KeyCode.E)) UnInteract(_lastSelectedGO);
			else if (Input.GetKeyDown(KeyCode.Q)) DropItems();
		}
		
		private void DropItems()
		{
			_visibleSlotsMB.DropItems();
		}

		private void UpdateHintUI(GameObject collidedGO)
		{
			if(collidedGO == null)
			{
				hintText.text = "";
				return;
			}

			if (collidedGO.TryGetComponent(out GroundItemMB groundItemMB))
			{
				hintText.text = groundItemMB.itemSO.hintText;
			}
			else if(collidedGO.TryGetComponent(out CollectionPointMB collectionPointMB))
			{
				hintText.text = collectionPointMB.hintText;
			}
			else
			{
				hintText.text = "";
			}
		}

		private void ScrollSlots()
		{
			var scrollDelta = (int)Input.mouseScrollDelta.y;

			if (scrollDelta != 0)
			{
				_visibleSlotsMB.ChangeSelectedSlot(scrollDelta);
			}
		}

		private RaycastHit MakeRaycast(Vector3 originPos, Vector3 direction, float maxDistance)
		{
			Physics.Raycast(originPos, direction, out RaycastHit hit, maxDistance);
			Debug.DrawRay(originPos, direction * hit.distance, Color.yellow);

			return hit;
		}

		private void Interact(GameObject itemToInteract)
		{
			_lastSelectedGO = itemToInteract;
			
			if(itemToInteract != null)
			{
				if (itemToInteract.TryGetComponent(out GroundItemMB groundItemMB))
				{
					MoveGroundItemToInventorySlot(itemToInteract);
				}

				else if (itemToInteract.TryGetComponent(out CollectionPointMB collectionPoint))
				{
					InteractWithCollectionPoint(itemToInteract.GetComponent<HiddenSlotsMB>(), collectionPoint);
				}

				else if (itemToInteract.TryGetComponent(out RotatableMirrorMB rotatableMirror))
				{
					rotatableMirror.SetActive(true);
				}
			}
		}

		private void UnInteract(GameObject itemToUnInteract)
		{
			if(itemToUnInteract != null)
			{
				if (itemToUnInteract.TryGetComponent(out RotatableMirrorMB rotatableMirror))
				{
					rotatableMirror.SetActive(false);
				}
			}
		}

		private void InteractWithCollectionPoint(HiddenSlotsMB hiddenSlotsMB, CollectionPointMB collectionPoint)
		{
			var selectedItemSO = _visibleSlotsMB.selectedSlot.ItemObject?.Item.ItemSO;

			if (selectedItemSO == null) return;

			if (collectionPoint.CanAddItemToCollectionPoint(selectedItemSO))
			{
				MoveItem(hiddenSlotsMB);
			}
		}

		private void MoveGroundItemToInventorySlot(GameObject groundItem)
		{
			if (_isMoving) return;

			var itemSO = groundItem.GetComponent<GroundItemMB>().itemSO;

			var itemObject = new ItemObject(itemSO);

			var isMoved = _visibleSlotsMB.AddItem(itemObject);

			if (isMoved)
			{
				_isMoving = true;
				
				if (groundItem.TryGetComponent<EarthQuakeMB>(out var earthQuake))
					DestroyGroundItem(groundItem, earthQuake);
				else
					DestroyGroundItem(groundItem);
			}
		}

		private void DestroyGroundItem(GameObject groundItem, EarthQuakeMB earthQuakeMB)
		{
			earthQuakeMB.EarthQuake();
			groundItem.GetComponent<MeshRenderer>().enabled = false;
			DestroyGroundItem(groundItem, earthQuakeMB.shakeTime/2);
		}

		private void DestroyGroundItem(GameObject groundItem, float delayTime = 0f)
		{
			StartCoroutine(DestroyExec(groundItem, delayTime));
		}

		private IEnumerator DestroyExec(GameObject groundItem, float delayTime)
		{
			yield return new WaitForSeconds(delayTime);

			if(groundItem.TryGetComponent<SpawnObjectsInScene>(out var spawnObjectsInSceneMB))
			{
				spawnObjectsInSceneMB.SpawnObjects();
			}
			if (groundItem.TryGetComponent<DespawnObjectsInScene>(out var despawnObjectsInSceneMB))
			{	
				despawnObjectsInSceneMB.DespawnObjects();
			}

			Destroy(groundItem);
			_isMoving = false;
		}

		private void MoveItem(HiddenSlotsMB slotsToBeMoved)
		{	
			var selectedSlot = _visibleSlotsMB.selectedSlot;

			var isMoved = slotsToBeMoved.AddItem(selectedSlot.ItemObject);

			if (isMoved)
			{
				_visibleSlotsMB.ClearSelectedSlot();
			}
		}
	}
}