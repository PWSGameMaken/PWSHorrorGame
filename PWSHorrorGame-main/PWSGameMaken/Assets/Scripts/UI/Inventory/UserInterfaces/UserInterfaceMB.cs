/*
* Grobros
* https://github.com/GroBro-s
*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInterfaceMB : MonoBehaviour
{
	#region Unity Functions
	void Start()
	{
		AddEvents();
	}

	private void AddEvents()
	{
		var eventTrigger = GetComponent<EventTrigger>();
		AddEvent(eventTrigger, EventTriggerType.PointerEnter, delegate { MouseObject.OnEnterInterface(this); });
		AddEvent(eventTrigger, EventTriggerType.PointerExit, delegate { MouseObject.OnExitInterface(); });
	}

	protected void AddEvent(EventTrigger trigger, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		var triggerEntry = new EventTrigger.Entry
		{
			eventID = type
		};
		triggerEntry.callback.AddListener(action);

		trigger.triggers.Add(triggerEntry);
	}
	#endregion
}