using UnityEngine;

public class Mirror : MonoBehaviour
{
	[SerializeField] private Transform originPos;
	[SerializeField] private Vector3 direction;
	[SerializeField] private int maxDistance;
	
	private void Update()
	{
		Physics.Raycast(originPos.position, direction, out RaycastHit hit, maxDistance);
		Debug.DrawRay(originPos.position, direction * hit.distance, Color.yellow);
	}
}

