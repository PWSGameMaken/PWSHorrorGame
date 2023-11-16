using UnityEngine;

public class IsRunningMB : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private bool _isRunning = false;

    void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
        _Anim.SetBool("IsRunning", false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if(!_isRunning)
            {
                ChangeMovementState(!_isRunning);
            }
        }
        else
        {
            if(_isRunning)
            {
                ChangeMovementState(!_isRunning);
            }
        }
    }

    private void ChangeMovementState(bool isRunning)
    {
		_Anim.SetBool("IsRunning", isRunning);
        _isRunning = isRunning;
	}
}