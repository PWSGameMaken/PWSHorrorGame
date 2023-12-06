using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactItem", menuName = "Inventory System/Items/ArtifactItem")]
[System.Serializable]
public class ArtifactSO : ItemSO
{
	public ArtifactSO()
	{
		animTag = AnimTag.HasVase;
	}
}