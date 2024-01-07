using UnityEngine;

public class FallRespawnMB : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		var collidedGO = other.gameObject;
		if(collidedGO.CompareTag("Player"))
		{
			RespawnSystemMB.instance.RespawnFromWeightPuzzle(collidedGO.transform);
		}
	}
}
