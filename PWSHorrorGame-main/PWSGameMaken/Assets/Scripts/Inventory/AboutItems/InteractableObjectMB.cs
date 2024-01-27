using UnityEngine;

public abstract class InteractableObjectMB : MonoBehaviour
{
	[SerializeField] private string hintText;

	public string HintText { get => hintText; set => hintText = value; }

	public void ObjectiveCompleted()
	{
		var _actionObjects = GetComponents<ActionObjectMB>();
		foreach (var _object in _actionObjects)
		{
			_object.Action();
		}
	}
}
