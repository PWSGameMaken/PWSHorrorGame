using UnityEngine;

public class FollowArea : AiTriggers
{
	private void OnTriggerExit(Collider other)
	{
		var otherGO = other.gameObject;
		if (otherGO.TryGetComponent<ITargetMB>(out var iTargetMB) && otherGO == targetGO)
		{
			monsterWithAiMB.FollowTarget(iTargetMB);
		}
	}
}
