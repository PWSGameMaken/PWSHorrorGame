using UnityEngine;

public class IsWalkingMB : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private bool _isWalking = false;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
        _Anim.SetBool("IsWalking", _isWalking);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
        {
			if (!_isWalking)
            {
                ChangeMovementState(!_isWalking);
            }
        }
        else
        {
            if (_isWalking) ChangeMovementState(!_isWalking);
        }

        if(_isWalking && Input.GetKey(KeyCode.LeftShift)) 
            ChangeMovementState(!_isWalking);
    }

    private void ChangeMovementState(bool isWalking)
    {
		_Anim.SetBool("IsWalking", isWalking);
		_isWalking = isWalking;
	}
}