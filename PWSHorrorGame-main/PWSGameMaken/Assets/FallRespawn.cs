using UnityEngine;

public class FallRespawn : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			RespawnSystemMB.instance.RespawnFromWeightPuzzle();
		}
	}
}
