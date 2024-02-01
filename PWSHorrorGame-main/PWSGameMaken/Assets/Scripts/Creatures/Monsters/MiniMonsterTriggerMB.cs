using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
		_audioManager.Play(MiniMonsterAudio.MiniMonsterFootsteps.ToString(), gameObject);
		_audioManager.PlayOneShot(MiniMonsterAudio.MiniMonsterScream.ToString(), gameObject);
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