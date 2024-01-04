using UnityEngine;

public class DespawnObjectsMB : ParentSpawnObjectsMB
{
	private bool isActivated = true;
	public override void Action()
	{
		SetActiveState(!isActivated);
		isActivated = !isActivated;
	}
}
