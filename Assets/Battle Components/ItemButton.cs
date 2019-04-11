using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	
	public Item item;
	public int index;
	public static bool inBattle;
	public GameObject messageLog;
	
	public ItemButton(Item item) {this.item = item;}

	private TextMeshProUGUI label;
	private Button button;
	
	void Awake() {
		label = gameObject.GetComponentInChildren<TextMeshProUGUI>();
		button = GetComponent<Button>();
	}

	void Start () {
		index = ItemStatic.instance.indexes.Dequeue();
		item = Party.GetItem(index);
		label.text = item.GetName();
		
		try {
		    messageLog = gameObject.transform.parent.parent.Find("Message Log").gameObject;
		} catch {
			messageLog = null;
		}
		if (!inBattle & !item.usableOut) {
			button.interactable = false;
		} else {
			button.interactable = true;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData e) {
		Description();
	}

	public void OnPointerExit(PointerEventData e) {
		Hide();
	}
	
	public void Consume () {
        Destroy(gameObject);		
	}
	
	public void Clicked () {
		Party.UseItem(index);
		try {
	        BattleStatic.instance.UseItem(item);
	    } catch {
			gameObject.transform.parent.parent.gameObject.GetComponent<Dungeon>().UseItem(item);
		}
	}
	
	void OnDestroy () {
	}
	
	public void Description () {
		ItemStatic.description.text = item.GetDescription();
	}
	
    public void Hide () {
		ItemStatic.description.text = "";
	}
}
