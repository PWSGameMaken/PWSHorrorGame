using UnityEngine;

public abstract class ActionsMB : MonoBehaviour
{
	[SerializeField] protected GameObject[] objects;

	public abstract void Action();
}
