using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactMB : MonoBehaviour
{
	[SerializeField] private GameObject rocksToSpawn;
	[SerializeField] private GameObject wallToCollapse;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			rocksToSpawn.SetActive(true);
			wallToCollapse.SetActive(false);
		}	
	}
}
