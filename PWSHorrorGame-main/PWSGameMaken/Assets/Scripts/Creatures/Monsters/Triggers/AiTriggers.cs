using UnityEngine;

public class AiTriggers : MonoBehaviour
{
	protected MonsterWithAiMB monsterWithAiMB;
	protected GameObject targetGO;

	private void Start()
	{
		monsterWithAiMB = GetComponentInParent<MonsterWithAiMB>();
		targetGO = monsterWithAiMB.target;
	}
}
