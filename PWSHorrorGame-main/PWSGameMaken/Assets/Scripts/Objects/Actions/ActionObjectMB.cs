using UnityEngine;

public abstract class ActionObjectMB : MonoBehaviour
{
	[SerializeField] protected GameObject[] objects;

	public abstract void Action();
}
