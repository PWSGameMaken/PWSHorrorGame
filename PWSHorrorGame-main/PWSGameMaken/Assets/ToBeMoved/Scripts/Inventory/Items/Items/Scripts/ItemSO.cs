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
	Artefact,
	Camera,
	Default
}


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
[System.Serializable]
public class ItemSO : ScriptableObject
{
	public GameObject prefab;
	public Sprite sprite;
	public bool stackable;
	public string itemName;
	public int id = -1;
	public ItemType type;
	[TextArea(15, 20)]
	public string description;
}