using UnityEngine;

public class DespawnObjectsInScene : MonoBehaviour
{
	[SerializeField] private GameObject[] objectsToDespawn;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			DespawnObjects();
		}
	}

	public void DespawnObjects()
	{
		for (int i = 0; i < objectsToDespawn.Length; i++)
		{
			if (objectsToDespawn[i].activeSelf)
			{
				objectsToDespawn[i].SetActive(false);
			}
		}
	}
}
