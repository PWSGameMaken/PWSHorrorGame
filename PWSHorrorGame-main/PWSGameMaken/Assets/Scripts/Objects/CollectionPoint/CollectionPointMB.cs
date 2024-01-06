using UnityEngine;

public abstract class CollectionPointMB : InteractableObjectMB
{
	protected bool isFull;

	private string _originalHintText;
	[SerializeField] private string _isFullHintText;


	protected void Start()
	{
		_originalHintText = hintText;
	}

	protected void ChangeHintText()
	{
		if (isFull) { hintText = _isFullHintText; }
		else if (!isFull) { hintText = _originalHintText; }
	}
}
