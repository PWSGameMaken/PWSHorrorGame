using UnityEngine;

public class LocalFogS : MonoBehaviour
{
	public float maxFogDensity;
	public float minFogDensity;

	public Collider colliderToMeasure;
	public Transform objectToMeasure;

	public int maxObjectDistance;
	public int minObjectDistance;

	private void Update()
	{
		var closestColliderPoint = colliderToMeasure.ClosestPoint(objectToMeasure.position);

		float distance = Vector3.Distance(objectToMeasure.position, closestColliderPoint);

		if (distance < maxObjectDistance)
		{
			var totalRange = maxObjectDistance - minObjectDistance;
			var newDensity = maxFogDensity * (distance / totalRange);

			if (newDensity >= minFogDensity)
			{
				RenderSettings.fogDensity = newDensity;
			}
			else
			{
				RenderSettings.fogDensity = 0;
			}
		}
		else
		{
			RenderSettings.fogDensity = maxFogDensity;
		}
	}
}
