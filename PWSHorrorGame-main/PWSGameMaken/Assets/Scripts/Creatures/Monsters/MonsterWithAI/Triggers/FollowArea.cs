using UnityEngine;

public class FollowArea : AiTriggers
{
	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.TryGetComponent<ITargetMB>(out var iTargetMB))
		{
			_monsterWithAiMB.FollowTarget(iTargetMB);
		}
	}
}
