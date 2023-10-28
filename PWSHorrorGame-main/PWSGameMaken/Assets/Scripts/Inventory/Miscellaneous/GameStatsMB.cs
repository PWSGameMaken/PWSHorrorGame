/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;

public class GameStatsMB : MonoBehaviour
{
    #region Variables
    public Transform collectables;

	[SerializeField] private GameObject rocksToSpawn;
	[SerializeField] private GameObject wallToCollapse;

	#endregion

	#region Unity Functions
	public void CollapseMap()
	{
		print("collapse map is called");
		//yield return new WaitForSeconds(1);
		rocksToSpawn.SetActive(true);
		wallToCollapse.SetActive(false);
	}
	#endregion
}