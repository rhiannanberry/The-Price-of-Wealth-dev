using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaButton : MonoBehaviour {

	private Toggle toggle;
 
	private void Start()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(OnToggleValueChanged);
	}

	private void OnToggleValueChanged(bool isOn)
	{
		ColorBlock cb = toggle.colors;
		if (isOn)
		{
			cb.normalColor = Color.gray;
			cb.highlightedColor = Color.gray;
		}
		else
		{
			cb.normalColor = Color.white;
			cb.highlightedColor = Color.white;
		}
		toggle.colors = cb;
	}
	
	public void Enter (string location) {
		Areas.location = location;
		SceneManager.LoadScene("Dungeon");
	}
}
