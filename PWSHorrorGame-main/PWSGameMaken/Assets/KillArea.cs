using UnityEngine;

public class KillArea : MonoBehaviour
{
	private Collider _collider;
	private EindMonsterMB _eindMonsterMB;
	private void Start()
	{
		_collider = GetComponent<Collider>();
		_eindMonsterMB = GetComponentInParent<EindMonsterMB>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			_eindMonsterMB.CollideWithPlayer();
		}
	}
}
