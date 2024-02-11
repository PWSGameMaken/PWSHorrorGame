using UnityEngine;

public class HuntArea : AiTriggers
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.TryGetComponent<ITargetMB>(out var iTargetMB))
		{
			_monsterWithAiMB.HuntTarget(iTargetMB);
		}
	}
}
