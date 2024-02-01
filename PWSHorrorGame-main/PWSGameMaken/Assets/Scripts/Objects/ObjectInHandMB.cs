using UnityEngine;

public class ObjectInHandMB : MonoBehaviour
{
	private GameObject _currentObjectInHand;
	private AnimMB _animMB;

	private void Start()
	{
		_animMB = GetComponentInParent<AnimMB>();
	}

	public void SetActive(ItemSO selectedItemSO, bool activeState)
	{
		if (activeState) 
		{
			Spawn(selectedItemSO); 
		}
		else
		{ 
			Despawn(selectedItemSO); 
		}
	}

	private void Spawn(ItemSO itemSO)
	{
		_currentObjectInHand = Instantiate(itemSO.inHandItemPrefab, transform);
		_animMB.SetAnimation(itemSO.animTag.ToString(), true);
	}

	private void Despawn(ItemSO itemSO)
	{
		Destroy(_currentObjectInHand);
		_animMB.SetAnimation(itemSO.animTag.ToString(), false);
	}
}
