using UnityEngine;

public class KillArea : AiTriggers
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.TryGetComponent<ITargetMB>(out var targetMB))
		{
			_monsterWithAiMB.KillTarget(targetMB);
		}
	}
}
