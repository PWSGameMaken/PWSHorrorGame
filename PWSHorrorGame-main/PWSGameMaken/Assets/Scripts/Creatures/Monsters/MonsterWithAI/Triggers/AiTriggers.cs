using UnityEngine;

public class AiTriggers : MonoBehaviour
{
	protected MonsterWithAiMB _monsterWithAiMB;

	private void Start()
	{
		_monsterWithAiMB = GetComponentInParent<MonsterWithAiMB>();
	}
}
