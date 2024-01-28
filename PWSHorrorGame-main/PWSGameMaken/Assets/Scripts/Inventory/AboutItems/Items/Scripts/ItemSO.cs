/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
[System.Serializable]
public class ItemSO : ScriptableObject
{
	public GameObject groundItemPrefab;
	public GameObject inHandItemPrefab;
	public Sprite sprite;
	public bool stackable;
	public int id = -1;
	[TextArea(15, 20)]
	public string description;
	public PlayerAnimations animTag;
	public AudioClip dropSound;
} 