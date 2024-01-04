using UnityEngine;

public class ObjectInHandMB : MonoBehaviour
{
	[SerializeField] private MovementAnimationMB _movementAnimationMB;
	private GameObject _currentObjectInHand;

	public void SetActive(ItemSO selectedItemSO, bool activeState)
	{
		if (activeState) { Spawn(selectedItemSO); }
		if (!activeState) { Despawn(); }
	}

	private void Spawn(ItemSO itemSO)
	{
		_currentObjectInHand = Instantiate(itemSO.inHandItemPrefab, transform);
	}

	private void Despawn()
	{
		Destroy(_currentObjectInHand);
	}
}
