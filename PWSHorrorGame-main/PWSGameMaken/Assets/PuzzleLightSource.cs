using UnityEditor;
using UnityEngine;

public class PuzzleLightSource : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxReflectionDistance = 100;
	public GameObject lightRayPrefab;

	private void OnDrawGizmos()
	{
		Handles.color = Color.red;
		Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 0.25f);
	}

	private void Update()
	{
		DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
	}

	private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
	{
		if (reflectionsRemaining == 0)
		{
			return;
		}

		Vector3 startingPos = position;

		Ray ray = new Ray(position, direction);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, maxReflectionDistance))
		{
			GameObject hitGO = hit.collider.gameObject;
			position = hit.point;
			if (hitGO.CompareTag("Mirror"))
			{
				direction = Vector3.Reflect(direction, hit.normal);              
			}
			else if (hitGO.CompareTag("CollectionPoint"))
			{
				hitGO.GetComponent<CollectionPoint>().ObjectiveCompleted();
				direction = Vector3.Reflect(direction, hit.normal);
			}
		}
		else
		{
			position += direction * maxReflectionDistance;
		}

		Debug.DrawLine(startingPos, position, Color.yellow);

		DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
	}
}


//[SerializeField] private Transform startingPos;
//[SerializeField] private Vector3 startingDir;

//private void Update()
//{
//	MakeRays(startingPos.position, startingDir);

//	Debug.DrawRay(startingPos.position, startingDir);
//}
//private void MakeRays(Vector3 position, Vector3 direction)
//{
//	if (Physics.Raycast(position, direction, out RaycastHit hit, Mathf.Infinity))
//	{
//		if(hit.collider.gameObject.CompareTag("Mirror"))
//		{
//			Vector3 incidenceAngle = hit.point - startingPos.position;
//			Vector3 reflectionAngle = Vector3.Reflect(incidenceAngle, hit.normal);
//			var targetPos = hit.transform.position;

//			Debug.DrawRay(hit.point, reflectionAngle * 100, Color.red);

//			MakeRays(targetPos, reflectionAngle);
//		}
//	}
//}
