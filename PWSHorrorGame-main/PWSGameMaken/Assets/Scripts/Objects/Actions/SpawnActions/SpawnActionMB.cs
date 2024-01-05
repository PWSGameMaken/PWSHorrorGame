public class SpawnActionMB : ParentSpawnActionsMB
{
	private bool _spawnObjects = true;
	public override void Action()
	{
		SetActiveState(_spawnObjects);
		_spawnObjects = !_spawnObjects;
	}
}