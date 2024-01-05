using UnityEngine;

public class DespawnActionMB : ParentSpawnActionsMB
{
	private bool _spawnObjects = false;
	public override void Action()
	{
		SetActiveState(_spawnObjects);
		_spawnObjects = !_spawnObjects;
	}
}
