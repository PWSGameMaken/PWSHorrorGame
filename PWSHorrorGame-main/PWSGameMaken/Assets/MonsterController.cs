using UnityEngine.AI;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	public float huntRadius = 10f;

	public Transform target;
	public NavMeshAgent agent;
	private void Start()
	{
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(target.transform.position);
	}

	private void Update()
	{
		agent.SetDestination(target.transform.position);

		
		float distance = Vector3.Distance(target.position, transform.position);
		if(distance < huntRadius)
		{
			print("The moster is close");
			//Activeer jaag modus
			FaceTarget();
		}
	}

	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
