using UnityEngine;

public class PuzzleLightSourceMB : MonoBehaviour
{
	public int maxReflectionCount = 5;
	public float maxReflectionDistance = 100;
	public LightRayMB[] lightRayMBs;
	private GameObject hitGO;

	private void Start()
	{
		lightRayMBs = new LightRayMB[maxReflectionCount];
	}

	//private void OnDrawGizmos()
	//{
	//	Handles.color = Color.red;
	//	Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawWireSphere(this.transform.position, 0.25f);

	//	if (!Application.isPlaying)
	//	{
	//		DrawPredictedGizmos(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
	//	}
	//}

	private void Update()
	{
		DrawPredictedReflectionPattern(
			gameObject, 
			transform.position + transform.forward * 0.75f, 
			transform.forward, 
			maxReflectionCount, 
			0);
	}

	private void DrawPredictedGizmos(Vector3 position, Vector3 direction, int reflectionsRemaining)
	{
		if(reflectionsRemaining == 0)
		{
			return;
		}

		Vector3 startingPosition = position;

		Ray ray = new Ray(position, direction);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, maxReflectionDistance))
		{
			direction = Vector3.Reflect(direction, hit.normal);
			position = hit.point;
		}
		else
		{
			position += direction * maxReflectionDistance;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawLine(startingPosition, position);

		DrawPredictedGizmos(position, direction, reflectionsRemaining - 1);
	}

	private void DrawPredictedReflectionPattern(GameObject originalGO, Vector3 position, Vector3 direction, int reflectionsRemaining, int index)
	{
		if (reflectionsRemaining == 0) return;

		Vector3 startingPos = position;
		Ray ray = new Ray(position, direction);
		var hasHit = Physics.Raycast(ray, out RaycastHit hit, maxReflectionDistance);

		if (hasHit)
		{
			OnHit(originalGO, hit, startingPos, ref position, ref direction, index);
		}
		else
		{
			OnMis(ref originalGO, ref position, ref direction);
		}

		Debug.DrawLine(startingPos, position, Color.yellow);

		DrawPredictedReflectionPattern(hitGO, position, direction, reflectionsRemaining - 1, ++index);
	}

	private void OnHit(GameObject originalGO, RaycastHit hit, Vector3 startPos, ref Vector3 position, ref Vector3 direction, int index)
	{
		hitGO = hit.collider.gameObject;
		position = hit.point;

		var lightRayMB = MakeLightRay(originalGO, startPos, position);

		if (lightRayMBs[index] == null)
		{
			lightRayMBs[index] = lightRayMB;
		}

		var tag =
			hitGO.CompareTag("Mirror") ? "Mirror"
			: hitGO.CompareTag("CollectionPoint") ? "CollectionPoint"
			: "None";

		switch (tag)
		{
			case "Mirror":
				direction = Vector3.Reflect(direction, hit.normal);
				break;

			case "CollectionPoint":
				hitGO.GetComponent<CollectionPointMB>().ObjectiveCompleted();
				break;

			default:
				DestroyLightRays(index);
				break;
		}
	}

	private void OnMis(ref GameObject originalGO, ref Vector3 position, ref Vector3 direction)
	{
		position += direction * maxReflectionDistance;
		hitGO = originalGO;
	}

	private LightRayMB MakeLightRay(GameObject originalGO, Vector3 startPos, Vector3 position)
	{
		if(originalGO.TryGetComponent<LightRayMB>(out var lightRayMB))
		{
			lightRayMB.SetActive(true);
			lightRayMB.SetTransform(startPos, position);

			return lightRayMB;
		}
		return null;
	}

	private void DestroyLightRays(int index)
	{
		for (int i = index + 1; i < lightRayMBs.Length; i++)
		{
			if (lightRayMBs[i] != null)
				lightRayMBs[i].SetActive(false);
		}
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