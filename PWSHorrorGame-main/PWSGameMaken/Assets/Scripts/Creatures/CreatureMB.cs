using UnityEngine;

public class CreatureMB : MonoBehaviour
{
	//[SerializeField] private Transform[] respawnPoints;
	//public Transform[] RespawnPoints { get => respawnPoints; }

	protected AnimMB AnimMB { get; private set; }
	protected AudioManager AudioManager { get; private set; }

	protected void Start()
	{
		AnimMB = GetComponentInChildren<AnimMB>();
		AudioManager = AudioManager.instance;
	}

	protected void FaceObject(Transform ObjectToRotate, Vector3 ObjectToFace, float rotationSpeed)
	{
		Vector3 direction = (ObjectToFace - ObjectToRotate.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		ObjectToRotate.rotation = Quaternion.Slerp(ObjectToRotate.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}
}
