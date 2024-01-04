/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;
public enum ItemType
{
	SugarCane,
	SharpSugarcane,
	Weight,
	HeavyWeight,
	Mushroom,
	PoisenousMushroom,
	Food,
	Artifact,
	Camera,
	Steen,
	ladder,
	SackWithStones,
	Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
[System.Serializable]
public class ItemSO : ScriptableObject
{
	public GameObject groundItemPrefab;
	public GameObject inHandItemPrefab;
	public Sprite sprite;
	public bool stackable;
	public string itemName;
	public int id = -1;
	public ItemType type;
	[TextArea(15, 20)]
	public string description;
	public AnimTag animTag;
} 