/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

public class GroundItemMB : MonoBehaviour
{
	private static Transform _collectables;
	public ItemSO itemSO;
	private static readonly int _power = 100;

	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_collectables = gameStatsMB.collectables;
	}

	public GroundItemMB(ItemSO itemSO)
	{
		this.itemSO = itemSO;
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		Vector3 forward = player.transform.TransformDirection(Vector3.forward);		

		var newGroundItem = Instantiate(itemSO.groundItemPrefab, GetSpawnPosition() + forward * 0.75f, itemSO.groundItemPrefab.transform.rotation, _collectables);
		newGroundItem.GetComponent<GroundItemMB>().itemSO = itemSO;


		newGroundItem.GetComponent<Rigidbody>().AddForce(forward * _power);

		return newGroundItem;
	}

	private static Vector3 GetSpawnPosition()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var playerPos = player.transform.position;

		return new Vector3(playerPos.x, playerPos.y + 1.8f, playerPos.z);
	}
}