using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {
	
	Item[] items;
	Item[] loot;
	public GameObject toggle;
	public GameObject finish;
	public GameObject itemButtonPrefab;
	public Queue<int> indexes = new Queue<int>();
	public GameObject lootContainer;
	public GameObject bagContainer;
	int selected;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Awake () {
		selected = 0;
		items = Party.GetItems();
		loot = Party.GetLoot();
		ItemToggle.Left();
		ItemToggle.switched = false;
		int i = 0;
		while (i < 10) {
			if (items[i] != null) {
				indexes.Enqueue(i);
			    Instantiate(toggle, gameObject.transform);
				selected++;
				GameObject o = Instantiate(itemButtonPrefab, bagContainer.transform);
				ItemLootButton oi = o.GetComponent<ItemLootButton>();
				oi.lootContainer = lootContainer.transform;
				oi.bagContainer = bagContainer.transform;
				oi.inBag = true;
				oi.item = items[i];
				o.GetComponentInChildren<TextMeshProUGUI>().text = items[i].GetName();
			}
			i++;
		}
		ItemToggle.Right();
		while (i < 20) {
		    if (loot[i - 10] != null) {
				indexes.Enqueue(i);
			    Instantiate(toggle, gameObject.transform);
				selected++;

				GameObject o = Instantiate(itemButtonPrefab, lootContainer.transform);
				ItemLootButton oi = o.GetComponent<ItemLootButton>();
				oi.lootContainer = lootContainer.transform;
				oi.bagContainer = bagContainer.transform;
				oi.inBag = false;
				oi.item = loot[i - 10];
				o.GetComponentInChildren<TextMeshProUGUI>().text = loot[i - 10].GetName();

		    }
			i++;
		}
		finish = gameObject.transform.Find("Confirm Win").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (selected > 10) {
			finish.GetComponent<Button>().enabled = false;
		} else {
			finish.GetComponent<Button>().enabled = true;
		}
	}
	
	public void ChangeNum(int i) {selected += i;}
	
	public void Finish () {
		Item[] newItems = new Item[10];
		int index = 0;
		foreach (Transform child in transform) {
		    if (child.tag.Equals("Temp")) {
			    if (child.gameObject.GetComponent<Toggle>().isOn) {
				    newItems[index] = child.gameObject.GetComponent<ItemToggle>().item;
				    index++;
			    }
		    }
		}
		Party.SetItems(newItems);
		gameObject.transform.parent.Find("Message Log").gameObject.GetComponent<MessageLog>().SetMessage("");
		//Party.PostBattle();
		//SceneManager.LoadScene(Party.area);
	}
	
	public void Confirm () {
		Item[] newItems = new Item[10];
		int index = 0;
		foreach (Transform child in transform) {
		    if (child.tag.Equals("Temp")) {
			    if (child.gameObject.GetComponent<Toggle>().isOn) {
				    newItems[index] = child.gameObject.GetComponent<ItemToggle>().item;
				    index++;
			    }
		    }
		}
		Party.SetItems(newItems);
		Party.ClearLoot();
	}
	
	
}
