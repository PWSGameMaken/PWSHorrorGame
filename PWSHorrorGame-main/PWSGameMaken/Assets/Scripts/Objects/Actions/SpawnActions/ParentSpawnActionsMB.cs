public abstract class ParentSpawnActionsMB : ActionsMB
{
	protected void SetActiveState(bool activeState)
	{
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].SetActive(activeState);
		}
	}
}
