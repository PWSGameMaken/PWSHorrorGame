public class SpawnObjectsMB : ParentSpawnObjectsMB
{
	private bool isActivated = false;
	public override void Action()
	{
		SetActiveState(!isActivated);
		isActivated = !isActivated;
	}
}