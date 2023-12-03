using UnityEngine;

public class LocalFogMB : MonoBehaviour
{
	[SerializeField] private float minFogDensity;
	[SerializeField] private float maxFogDensity;

	[SerializeField] private Collider colliderToMeasure;
	[SerializeField] private Transform objectToMeasure;

	[SerializeField] private int maxObjectDistance;
	[SerializeField] private int minObjectDistance;

	private void Update()
	{
		var closestColliderPoint = colliderToMeasure.ClosestPoint(objectToMeasure.position);
		float distance = Vector3.Distance(objectToMeasure.position, closestColliderPoint);

		if (distance < maxObjectDistance)
		{
			ChangeFogDensity(distance);
		}
		else
		{
			SetFogDensity(maxFogDensity);
		}
	}

	private void ChangeFogDensity(float distance)
	{
		var totalRange = maxObjectDistance - minObjectDistance;
		var newDensity = maxFogDensity * (distance / totalRange);

		if (newDensity >= minFogDensity)
		{
			SetFogDensity(newDensity);
		}
		else
		{
			SetFogDensity(0f);
		}
	}

	private void SetFogDensity(float newDensity)
	{
		RenderSettings.fogDensity = newDensity;
	}
}
