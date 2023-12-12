using Unity.VisualScripting;
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
    [SerializeField] private Animator _Anim;
    [SerializeField] private AudioSource Sounds;
    [SerializeField] public AudioClip WalkSound;
    private bool _walking = false;
    private bool soundPlayed = false;

    private void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
        _Anim.SetBool(AnimTag.IsWalking.ToString(), _walking);
    }

    private void Update()
    {
        var isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        var toggleWalking =
            !_walking && isWalking
            || _walking && !isWalking
            || _walking && Input.GetKey(KeyCode.LeftShift);

        if (toggleWalking)
        {
            ChangeMovementState(AnimTag.IsWalking, !_walking);
        }
        if (isWalking)
        {
            if (!soundPlayed)
            {
                Sounds.PlayOneShot(WalkSound);
                soundPlayed = true;
            }
            
        }
        else 
        {
            if (!isWalking)
            {
                if (soundPlayed)
                {
                    Sounds.Stop();
                    soundPlayed = false;
                }
            }
            
        }

    }

    private void ChangeMovementState(AnimTag activity, bool isWalking)
    {
        _Anim.SetBool(activity.ToString(), isWalking);
        _walking = isWalking;
    }

    public void ChangeHandAnimationState(AnimTag animTag, bool state)
    {
        _Anim.SetBool(animTag.ToString(), state);
    }
}