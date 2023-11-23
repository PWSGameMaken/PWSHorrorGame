using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
	public GameObject lightRay;
	public bool isActivated;

	public void ActivateLightRay()
	{
		if (isActivated) { return; }
		
		lightRay.SetActive(true);
		isActivated = true;
	}

	public void DeactivateLightRay()
	{
		if (!isActivated) { return; }	

		lightRay.SetActive(false); 
		isActivated = false;
	}
	//Hiervoor kan ook in 1 functie het tegenovergestelde van isActivated worden gebruikt.
}
