public class LightCollectionPoint : CollectionPointMB, IInteractWithoutSlot
{
	public void Interact()
	{
		ChangeHintText();
		ObjectiveCompleted();
	}
}
