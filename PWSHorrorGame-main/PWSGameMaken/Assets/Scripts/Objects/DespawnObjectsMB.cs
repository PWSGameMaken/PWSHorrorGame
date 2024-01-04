using UnityEngine;

public class DespawnObjectsMB : ParentSpawnObjectsMB
{
	public override void Action()
	{
		SetActiveState(false);
	}
}
