using UnityEngine;

public class SpawnObjectsInScene : MonoBehaviour
{
	[SerializeField] private GameObject[] objectsToSpawn;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			SpawnObjects();
		}
	}

	public void SpawnObjects()
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
