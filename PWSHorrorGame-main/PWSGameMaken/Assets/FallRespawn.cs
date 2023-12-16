using UnityEngine;

public class FallRespawn : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			RespawnSystem.instance.RespawnFromWeightPuzzle();
		}
	}
}
