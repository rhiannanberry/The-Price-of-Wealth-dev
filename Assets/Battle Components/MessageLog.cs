using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MessageLog : MonoBehaviour {
	
	public TextMeshProUGUI message;
	public LinkedList<string> previous;

	// Use this for initialization
	void Start () {
		SetMessage("");
	    previous = new LinkedList<string>();
		previous.AddFirst("");
	}
	
	public void SetMessage(string contents) {
		message.text = contents;
		if (contents != "") {
		    previous.AddFirst(contents);
		}
	}
}
