using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OnEnterWalk : MonoBehaviour
{
    public Transform objectToMove; // Reference to the object you want to move
    public Vector3 moveDirection = new Vector3(0, 0, 1); // Change this to the desired movement direction
    public float moveSpeed = 5f; // Change this to the desired movement speed
    public GameObject Trigger1; 
    private bool playerInsideTrigger = false;
    private bool playerWasInside = false;

    private void OnTriggerEnter(Collider other)
    {   
    playerInsideTrigger = true;
        playerWasInside = true;

    }

    private void OnTriggerExit(Collider other)
    {
    playerInsideTrigger = false;
        Object.Destroy(Trigger1);
    }

    private void Update()
    {
        if (playerInsideTrigger)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        // Move the object based on the specified direction and speed
        objectToMove.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}