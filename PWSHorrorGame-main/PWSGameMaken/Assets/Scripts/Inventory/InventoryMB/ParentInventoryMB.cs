/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using UnityEngine;

namespace Inventory
{
	public abstract class ParentInventoryMB : MonoBehaviour
	{
		#region variables
		[SerializeField] protected ItemDatabaseSO itemDatabaseSO;
		
		public GameObject inventoryUI;
		#endregion
	}
}
