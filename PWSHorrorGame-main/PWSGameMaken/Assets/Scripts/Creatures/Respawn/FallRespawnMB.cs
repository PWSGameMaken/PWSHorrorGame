using UnityEngine;

public class FallRespawnMB : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			var respawnPos = other.GetComponent<PlayerMB>().WeightPuzzleRespawnPos;
			RespawnSystemMB.Respawn(other.transform, respawnPos);
		}
	}
}
