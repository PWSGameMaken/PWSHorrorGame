using UnityEngine;

public class LightRayMB : MonoBehaviour
{
	[SerializeField] private GameObject lightRay;
	[SerializeField] private bool isActivated;

	public void SetActive(bool status)
	{
		if (isActivated == status) { return; }

		lightRay.SetActive(status);
		isActivated = status;
	}

	public void SetTransform(Vector3 beginPos, Vector3 endPos)
	{
		SetPosition(beginPos, endPos);
		SetScale(beginPos, endPos);
		SetRotation(endPos);
	}

	private void SetPosition(Vector3 beginPos, Vector3 endPos)
	{
		var middlePos = (beginPos + endPos) / 2;

		lightRay.transform.position = middlePos;
	}

	private void SetScale(Vector3 beginPos, Vector3 endPos)
	{
		var lengthOfRay = Vector3.Distance(beginPos, endPos);
		lightRay.transform.localScale = new Vector3(1, 1, lengthOfRay);
	}

	private void SetRotation(Vector3 endPos)
	{
		lightRay.transform.LookAt(endPos);
	}
}
