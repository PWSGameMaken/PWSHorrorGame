/*
* Grobros
* https://github.com/GroBro-s/MorbidMarshmallow
*/
using UnityEngine;

public class GameStatsMB : MonoBehaviour
{
	#region Singleton

	public static GameStatsMB instance;

	private void Awake()
	{
		instance = this;
	}
	#endregion

	#region Variables
	public ItemDatabaseSO itemDatabaseSO;
    #endregion
}