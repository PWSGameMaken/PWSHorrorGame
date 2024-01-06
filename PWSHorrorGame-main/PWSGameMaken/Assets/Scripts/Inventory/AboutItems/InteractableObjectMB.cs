using UnityEngine;

public abstract class InteractableObjectMB : MonoBehaviour
{
	public string hintText;


	public void ObjectiveCompleted()
	{
		var _actionObjects = GetComponents<ActionObjectMB>();
		foreach (var _object in _actionObjects)
		{
			_object.Action();
		}
	}
}
