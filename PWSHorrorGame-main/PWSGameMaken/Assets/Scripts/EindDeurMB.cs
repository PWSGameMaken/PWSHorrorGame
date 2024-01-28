using UnityEngine;

public class EindDeurMB : MonoBehaviour
{
	[SerializeField] private ItemCollectionPoint[] _collectionPoints;
	[SerializeField] private EndGameMenu _endGameMenu;

	public void CheckCollectionPointsFilled()
	{
		foreach (var collectionPoint in _collectionPoints)
		{
			if (!collectionPoint.isFull) return;
		}

		EndGame();
	}

	private void EndGame()
	{
		_endGameMenu.SetActiveMenu();
	}
}
