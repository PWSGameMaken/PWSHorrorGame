using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnMonster : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject monster;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == player)
		{
			monster.SetActive(false);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject == player)
		{
			monster.SetActive(true);
			Respawnsystem
		}
	}
}
