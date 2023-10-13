/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	public class UninteractableInventoryMB : ParentInventoryMB
	{
		[SerializeField] private GameObject inventoryUI;

		protected override void OnTriggerEnter(Collider collision)
		{
			inventoryUI.SetActive(true);
		}

		private void OnTriggerExit(Collider collision)
		{
			inventoryUI.SetActive(false);
		}
	}
}
