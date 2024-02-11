using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactItem", menuName = "Inventory System/Items/ArtifactItem")]
[System.Serializable]
public class ArtifactSO : ItemSO
{
	public ArtifactSO()
	{
		animTag = PlayerAnimations.HasVase;
		stackable = false;
	}
}