using UnityEngine;

public class HuntArea : AiTriggers
{
	private void OnTriggerEnter(Collider other)
	{
		var otherGO = other.gameObject;
		if (otherGO.TryGetComponent<ITargetMB>(out var iTargetMB) && otherGO == targetGO)
		{
			monsterWithAiMB.HuntTarget(iTargetMB);
		}
	}
}
