using UnityEngine;

public class ObjectInHandMB : MonoBehaviour
{
	[SerializeField] private GameObject _currentObjectInHand;
	public void Spawn(ItemSO itemSO)
	{
		_currentObjectInHand = Instantiate(itemSO.inHandItemPrefab, this.gameObject.transform);
	}

	public void Despawn()
	{
		if( _currentObjectInHand != null )
		{
			Destroy(_currentObjectInHand);
		}
	}
}
