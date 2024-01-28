using UnityEngine;

public class MiniMonsterTriggerMB : MonoBehaviour
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private Vector3 _moveDirection = new Vector3(0, 0, 1);
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay;

    private AudioManager _audioManager;
    private bool playerInsideTrigger = false;

	private void Start()
	{
        _audioManager = AudioManager.instance;
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
        _audioManager.Play("MiniMonsterFootsteps", gameObject);
        _audioManager.PlayOneShot("MiniMonsterScream", gameObject);
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