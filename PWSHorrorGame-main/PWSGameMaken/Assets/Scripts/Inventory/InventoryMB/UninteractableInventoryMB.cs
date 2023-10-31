/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

namespace Inventory
{
	public class UninteractableInventoryMB : ParentInventoryMB
	{
		protected void OnTriggerEnter(Collider collision)
		{
			inventoryUI.SetActive(true);
		}

		private void OnTriggerExit(Collider collision)
		{
			inventoryUI.SetActive(false);
		}
	}
}
