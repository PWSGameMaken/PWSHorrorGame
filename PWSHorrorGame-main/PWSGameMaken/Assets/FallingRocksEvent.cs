using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocksEvent : MonoBehaviour
{
	[SerializeField] private GameObject rocksToFall;
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			rocksToFall.SetActive(true);
		}
	}
}
