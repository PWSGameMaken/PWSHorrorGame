using UnityEditor;
using UnityEngine;

public class PuzzleLightSource : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxStepDistance = 100;

	private void OnDrawGizmos()
	{
		Handles.color = Color.red;
		Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 0.25f);

		DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
	}

	private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
	{
		if(reflectionsRemaining == 0)
		{
			return;
		}

		Vector3 startingPos = position;

		Ray ray = new Ray(position, direction);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, maxStepDistance))
		{
			GameObject hitGO = hit.collider.gameObject;
			position = hit.point;
            if (hitGO.CompareTag("Mirror"))
            {
				direction = Vector3.Reflect(direction, hit.normal);
				//position = hit.point;                
            }
			else if (hitGO.CompareTag("CollectionPoint"))
			{
				hitGO.GetComponent<CollectionPoint>().ObjectiveCompleted();
				direction = Vector3.Reflect(direction, hit.normal);
				//position = hit.point;
			}
		}
		else
		{
			position += direction * maxStepDistance;
		}

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(startingPos, position);

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





//private void Update()
//{
//	if (Physics.Raycast(startingPos.position, startingDir, out RaycastHit hit0, Mathf.Infinity))
//	{
//		Debug.DrawRay(startingPos.position, startingDir, Color.yellow);

//		if(hit0.collider.gameObject.CompareTag("Mirror"))
//		{
//			Vector3 incidenceAngle0 = hit0.point - startingPos.position;
//			Vector3 reflectionAngle0 = Vector3.Reflect(incidenceAngle0, hit0.normal);
//			var target0 = hit0.transform;

//			Debug.DrawRay(hit0.point, reflectionAngle0 * 100, Color.red);

//			if (Physics.Raycast(target0.position, reflectionAngle0, out RaycastHit hit1, Mathf.Infinity))
//			{
//				if (hit1.collider.gameObject.CompareTag("Mirror"))
//				{
//					Vector3 incidenceAngle1 = hit1.point - target0.position;
//					Vector3 reflectionAngle1 = Vector3.Reflect(incidenceAngle1, hit1.normal);
//					var target1 = hit1.transform;

//					Debug.DrawRay(hit1.point, reflectionAngle1 * 100, Color.green);

//					if (Physics.Raycast(target1.position, reflectionAngle1, out RaycastHit hit2, Mathf.Infinity))
//					{
//						if (hit2.collider.gameObject.CompareTag("Mirror"))
//						{
//							Vector3 incidenceAngle2 = hit2.point - target1.position;
//							Vector3 reflectionAngle2 = Vector3.Reflect(incidenceAngle1, hit2.normal);
//							var target2 = hit2.transform;

//							Debug.DrawRay(hit2.point, reflectionAngle2 * 100, Color.yellow);
//						}
//					}
//				}
//			}
//		}
//	}
//}