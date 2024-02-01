using UnityEngine;

public class HuntArea : MonoBehaviour
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
			_eindMonsterMB.HuntPlayer();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			_eindMonsterMB.FollowPlayer();
		}
	}
}
