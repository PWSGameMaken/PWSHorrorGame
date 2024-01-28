using UnityEngine;

public class FillEindDeur : ActionObjectMB
{
	public override void Action()
	{
		foreach (var _object in objects)
		{
			_object.GetComponent<EindDeurMB>().CheckCollectionPointsFilled();
		}
	}
}
