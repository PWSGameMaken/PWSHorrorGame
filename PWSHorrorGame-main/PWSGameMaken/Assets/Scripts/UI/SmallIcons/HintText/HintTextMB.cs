using TMPro;
using UnityEngine;

public class HintTextMB : MonoBehaviour
{
	private TextMeshProUGUI _hintText;

	private void Start()
	{
		_hintText = GetComponent<TextMeshProUGUI>();
	}

	public void UpdateHintUI(GameObject collidedGO)
	{
		_hintText.text = "";
		if (collidedGO != null)
		{
			if(collidedGO.TryGetComponent(out InteractableObjectMB interactableObjectMB))
			{
				_hintText.text = interactableObjectMB.hintText;
			}
		}
	}
}
