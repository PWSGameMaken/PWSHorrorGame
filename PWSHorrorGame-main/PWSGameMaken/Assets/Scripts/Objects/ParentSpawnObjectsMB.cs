public abstract class ParentSpawnObjectsMB : ActionObjectMB
{
	protected void SetActiveState(bool activeState)
	{
		for (int i = 0; i < objects.Length; i++)
		{
			objects[i].SetActive(activeState);
		}
	}
}
