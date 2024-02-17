using UnityEngine;

public class MiniMonsterTriggerMB : MonsterMB
{
    [SerializeField] private Transform _objectToMove;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _destroyDelay = 0f;

	private readonly Vector3 _moveDirection = new(0, 0, 1);
    private bool playerInsideTrigger = false;

	private new void Start()
	{
		base.Start();
	}

	private void Update()
	{
        if (playerInsideTrigger) MoveObject();
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
			Activate();
		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target)
		{
			Destroy(gameObject, _destroyDelay);
		}
	}

	private void Activate()
	{
		playerInsideTrigger = true;
		AudioManager.Play(MiniMonsterAudio.MiniMonsterFootsteps.ToString(), _objectToMove.gameObject);
		AudioManager.PlayOneShot(MiniMonsterAudio.MiniMonsterScream.ToString(), _objectToMove.gameObject);
	}

    private void MoveObject()
    {
        _objectToMove.Translate(_moveSpeed * Time.deltaTime * _moveDirection.normalized);
    }
}