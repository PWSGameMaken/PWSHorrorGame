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
    private Animator _anim;
    [SerializeField] private Audio _walkingSound;

    private bool _walking = false;

    private void Start()
    {
        _anim = GetComponent<Animator>();
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
            ChangeWalkingState(AnimTag.IsWalking, !_walking);
        }
    }

    private void ChangeWalkingState(AnimTag animTag, bool state)
    {
        _anim.SetBool(animTag.ToString(), state);
        _walkingSound.PlaySound(state);
        _walking = state;
    }

    public void ChangeHandAnimationState(AnimTag animTag, bool state)
    {
        _anim.SetBool(animTag.ToString(), state);
    }
}