using UnityEngine;

public class MovementAnimationMB : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private bool _isWalking = false;

    private void Start()
    {
        _Anim = gameObject.GetComponent<Animator>();
        _Anim.SetBool("IsWalking", _isWalking);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
        {
			if (!_isWalking)
            {
                ChangeMovementState("IsWalking", !_isWalking);
            }
        }
        else
        {
            if (_isWalking) ChangeMovementState("IsWalking", !_isWalking);
        }

        if(_isWalking && Input.GetKey(KeyCode.LeftShift))
        {
            ChangeMovementState("IsWalking", !_isWalking);
        }
    }

    private void ChangeMovementState(string nameOfBool, bool isWalking)
    {
		_Anim.SetBool(nameOfBool, isWalking);
		_isWalking = isWalking;
	}

    public void ChangeHandAnimationState(ItemSO itemSO, bool state)
    {
		if (itemSO.type == ItemType.Steen)
		{
			_Anim.SetBool("HasStone", state);
		}
		else if (itemSO.type == ItemType.Artifact)
		{
			_Anim.SetBool("HasVase", state);
		}
	}
}