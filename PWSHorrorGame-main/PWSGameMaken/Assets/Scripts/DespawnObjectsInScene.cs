using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObjectsInScene : MonoBehaviour
{
	[SerializeField] private GameObject[] objectsToDespawn;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			for (int i = 0; i < objectsToDespawn.Length; i++)
			{
				if (!objectsToDespawn[i].activeSelf)
				{
					objectsToDespawn[i].SetActive(false);
				}
			}
		}
	}
}
