using UnityEngine;

public class MiniMonsterTriggerMB : MonoBehaviour
{
    private PlaySounds _playSounds;
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay;

    private bool playerInsideTrigger = false;

	private void Start()
	{
		_playSounds = GetComponent<PlaySounds>();   
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            _playSounds.ActivateSounds(true);
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, _destroyDelay);
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