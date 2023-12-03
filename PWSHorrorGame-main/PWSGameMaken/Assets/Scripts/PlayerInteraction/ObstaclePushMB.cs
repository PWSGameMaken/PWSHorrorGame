using UnityEngine;

public class ObstaclePushMB : MonoBehaviour
{
	[SerializeField] private float _forceMagnitude;
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Rigidbody rb = hit.collider.attachedRigidbody;

		if(rb != null)
		{
			Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
			forceDirection.y = 0;
			forceDirection.Normalize();

			rb.AddForceAtPosition(forceDirection * _forceMagnitude, transform.position, ForceMode.Impulse);
		}
	}
}
