using UnityEngine;

public class MiniMonsterTriggerMB : MonoBehaviour
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay;

    private bool playerInsideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            Destroy(this.gameObject, _destroyDelay);
        }
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
        _objectToMove.Translate(_moveSpeed * Time.deltaTime * _moveDirection.normalized);
    }
}