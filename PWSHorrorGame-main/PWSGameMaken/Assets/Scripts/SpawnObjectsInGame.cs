using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsInGame : MonoBehaviour
{
	[SerializeField] private GameObject[] objectsToSpawn;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{	
			for(int i = 0; i < objectsToSpawn.Length; i++)
			{
				if (!objectsToSpawn[i].activeSelf)
				{
					GetComponent<EarthQuakeMB>().EarthQuake();
					objectsToSpawn[i].SetActive(true);
				}
			}
		}
	}
}
