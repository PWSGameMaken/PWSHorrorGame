public class LightCollectionPoint : CollectionPointMB, IInteractWithoutSlot
{
	private new void Start()
	{
		base.Start();
	}

	public void Interact()
	{
		ObjectiveCompleted();
	}
}
