using UnityEngine;

public class MiniMonsterTriggerMB : MonoBehaviour
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay;

    private AudioSource[] _audioSource;
    private bool playerInsideTrigger = false;

	private void Start()
	{
        _audioSource = GetComponentsInChildren<AudioSource>();
	}

	private void Update()
	{
        if (playerInsideTrigger) MoveObject();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			Activate();
		}
    }

	private void Activate()
	{
		playerInsideTrigger = true;
		foreach (var source in _audioSource) { source.Play(); }
	}

	private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, _destroyDelay);
        }
    }

    private void MoveObject()
    {
        _objectToMove.Translate(_moveSpeed * Time.deltaTime * _moveDirection.normalized);
    }
}