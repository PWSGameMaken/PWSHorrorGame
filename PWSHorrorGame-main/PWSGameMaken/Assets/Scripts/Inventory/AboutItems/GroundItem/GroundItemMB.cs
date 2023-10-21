/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

public class GroundItemMB : MonoBehaviour, ISerializationCallbackReceiver
{	
	private static Transform _collectables;
	private static GameObject _itemPrefab;

	public ItemSO itemSO;

	private void Start()
	{
		var gameStatsMB = GameObject.Find("GameController").GetComponent<GameStatsMB>();
		_itemPrefab = gameStatsMB.groundItemPrefab;
		_collectables = gameStatsMB.collectables;
	}

	public GroundItemMB(ItemSO itemSO)
	{
		this.itemSO = itemSO;
	}

	public static GameObject Create(ItemSO itemSO)
	{
		var newGroundItem = Instantiate(itemSO.prefab, GetSpawnPosition(), itemSO.prefab.transform.rotation, _collectables);

		newGroundItem.GetComponent<GroundItemMB>().itemSO = itemSO;

		return newGroundItem;
	}

	private static Vector3 GetSpawnPosition()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		var playerPos = player.transform.position;

		return new Vector3(playerPos.x + GetOffset(), playerPos.y + 0.5f, playerPos.z + GetOffset());
	}

	private static float GetOffset()
	{
		var offset = Random.Range(-2f, 2f);

		//offset needs to be more than 1 unit from player	
		while (offset > -1 && offset < 1)
		{
			offset = Random.Range(-2f, 2f);
		}

		return offset;
	}

	public void OnAfterDeserialize()
	{

	}

	public void OnBeforeSerialize()
	{
//#if UNITY_EDITOR
		//GetComponentInChildren<SpriteRenderer>().sprite = itemSO.sprite;
		//EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
//#endif
	}
}