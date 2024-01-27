using UnityEngine;

public enum AnimTag
{
    NoAnimation,
    IsWalking,
    HasVase,
    HasStone
}

public class PlayerMovementAnimationMB : MonoBehaviour
{
	[SerializeField] private AudioSource _soundSource;
    private Animator _anim;
    private bool _walking = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        SetAnimation(AnimTag.IsWalking, _walking);
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
            ToggleWalking(!_walking);
        }
    }

    private void ToggleWalking(bool state)
    {
        SetAnimation(AnimTag.IsWalking, state);
        //ToggleWalkingSounds(state);
        _walking = state;
    }

    public void SetAnimation(AnimTag animTag, bool state)
    {
        _anim.SetBool(animTag.ToString(), state);
    }

    //private void ToggleWalkingSounds(bool state)
    //{
    //    if(state) _soundSource.Play();
    //    else _soundSource.Stop();
    //}
}