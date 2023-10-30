/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
	public abstract class ParentInventoryMB : MonoBehaviour
	{
		#region variables
		[SerializeField] protected ItemDatabaseSO itemDatabaseSO;
		#endregion

		#region Unity Methods
		//protected abstract void OnTriggerEnter(Collider collision);
		#endregion
	}
}
