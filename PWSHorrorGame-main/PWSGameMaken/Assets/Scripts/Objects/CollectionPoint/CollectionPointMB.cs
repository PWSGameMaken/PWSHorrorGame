using UnityEngine;

public abstract class CollectionPointMB : InteractableObjectMB
{
	private string _originalHintText;
	[SerializeField] private string _isFullHintText;
	protected bool isFull;

	private void Start()
	{
		_originalHintText = hintText;
	}

	public void ChangeHintText()
	{
		if (isFull) { hintText = _originalHintText; }
		if(!isFull) { hintText = _isFullHintText; }
	}
}
