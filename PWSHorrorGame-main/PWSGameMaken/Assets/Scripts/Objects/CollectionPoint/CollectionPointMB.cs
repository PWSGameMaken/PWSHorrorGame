using UnityEngine;

public abstract class CollectionPointMB : InteractableObjectMB
{
	private string _originalHintText;
	[SerializeField] private string _isFullHintText;
	[HideInInspector] public bool isFull;

	private void Start()
	{
		_originalHintText = HintText;
	}

	public void ChangeHintText()
	{
		if (isFull) { HintText = _originalHintText; }
		if(!isFull) { HintText = _isFullHintText; }
	}
}
