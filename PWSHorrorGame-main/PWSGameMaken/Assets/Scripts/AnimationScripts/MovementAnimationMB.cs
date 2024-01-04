using UnityEngine;

public enum AnimTag
{
    NoAnimation,
    IsWalking,
    HasVase,
    HasStone
}

public class MovementAnimationMB : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] public AudioClip walkSound;

    private bool _walking = false;
    private bool _soundPlayed = false;

    private void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _anim.SetBool(AnimTag.IsWalking.ToString(), _walking);
    }

    private void Update()
    {
        var isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        var toggleWalking =
            !_walking && isWalking
            || _walking && !isWalking
            //Deze lijn moet weg worden gehaald wanneer we geen sprint meer hebben! (Onder)
            || _walking && Input.GetKey(KeyCode.LeftShift);

        if (toggleWalking)
        {
            ChangeMovementState(AnimTag.IsWalking, !_walking);
        }
        if (isWalking)
        {
            if (!_soundPlayed)
            {
                PlaySound(true);
            }

        }
        else
        {
            if (_soundPlayed)
            {
                PlaySound(false);
            }
        }
    }

    private void ChangeMovementState(AnimTag animTag, bool state)
    {
        _anim.SetBool(animTag.ToString(), state);
        _walking = state;
    }

    public void ChangeHandAnimationState(AnimTag animTag, bool state)
    {
        _anim.SetBool(animTag.ToString(), state);
    }

    private void PlaySound(bool setPlay)
    {
		if(setPlay == true) { _soundSource.Play(); }
        else { _soundSource.Stop(); }

		_soundPlayed = setPlay;
	}
}