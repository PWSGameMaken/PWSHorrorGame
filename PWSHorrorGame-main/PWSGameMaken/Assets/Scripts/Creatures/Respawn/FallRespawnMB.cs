using UnityEngine;

public class FallRespawnMB : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			RespawnSystemMB.instance.RespawnFromWeightPuzzle();
		}
	}
}
