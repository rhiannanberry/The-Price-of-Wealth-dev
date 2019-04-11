﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMenu : MonoBehaviour {
	public Transform membersButtonGroup;
	public Transform enemiesButtonGroup;

	public Text status;
	public Button swap;
	public Button cancel;
	public Button special;
	public Button kick;

	public Transform specialMenu;
	public Button StandardS;
	public Button SupportS;
	public int active;
	public Item item;
	public Special currentSpecial;
	public bool replacing;
	public Dungeon dungeon;

	[HideInInspector]
	Button[] chars;
	Button[] enemies;

	void Awake() {
		chars = membersButtonGroup.GetComponentsInChildren<Button>();
		if (enemiesButtonGroup != null)
			enemies = enemiesButtonGroup.GetComponentsInChildren<Button>();
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnEnable () {
		SetActive(Party.GetActive());

		for (int i = 0; i < 4; i++) {
			Button c = chars[i];
			Debug.Log(Party.GetActive());
			if(Party.GetCharacter(i) != null) {
				c.GetComponentInChildren<TextMeshProUGUI>().text = Party.GetCharacter(i).GetName();
				c.interactable = true;
				if (i == Party.GetActive() - 1) {
					c.Select();
					c.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
				} else {
					c.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
				}
			} else {
				c.interactable = false;
			}
			if (enemiesButtonGroup != null) 
			{
				Button b = enemies[i];
				if (Party.GetEnemy(i) != null) {
					b.GetComponentInChildren<TextMeshProUGUI>().text = Party.GetEnemy(i).type;
					b.interactable = true;
				} else {
					b.interactable = false;	
				}
			}
		}
		
		if (replacing) {
			swap.interactable = false;
		}
	}
	
	void OnDisable() {SetActive(1);}
	
	public Character GetActive () {
		return Party.GetCharacter(active - 1);
	}
	
	public void SetActive(int i) {
		active = i;

		for (int j = 0; j < 4; j++) {
			Button c = chars[j];
			
			c.GetComponent<Outline>().enabled = false;
			if (enemiesButtonGroup != null) {
				Button b = enemies[j];
				b.GetComponent<Outline>().enabled = false;
			}
		}
		if (i <=4 ) {
			chars[i-1].GetComponent<Outline>().enabled = true;
		} else {
			enemies[i-5].GetComponent<Outline>().enabled = true;
		}

		if (i < 5) {
		    status.text = Party.GetCharacter(i - 1).StatusText();
		} else {
			status.text = Party.GetEnemy(i - 5).StatusE();
		}
		if (i < 5 && ((Party.members[i - 1].GetSpecial().usableOut && 
		    Party.GetSP() >= Party.members[i-1].GetSpecial().GetCost()) || (Party.members[i - 1].GetSupportSpecial().usableOut && 
		        Party.GetSP() >= Party.members[i-1].GetSupportSpecial().GetCost()))) {
			special.interactable = true;
		} else {
			special.interactable = false;
		}
		if (i == 1) {
		    kick.interactable = false;
		} else {
			kick.interactable = true;
		}
		if (replacing) {
			swap.interactable = true;
		}

		//Swapping menu
		if (item == null && currentSpecial == null) {
		    if ((i == Party.playerSlot && !replacing) || i >= 5 || Party.GetPlayer().GetGooped()) {
	    		swap.interactable = false;
    		} else {
			    if (Party.members[i-1].GetAlive() && Party.members[i - 1].status.possessed == 0 && Party.members[i - 1].status.gooped == false) {	
		    	    swap.interactable = true;
	    		} else {
    				swap.interactable = false;
			    }
		    }
		} else if (currentSpecial != null && !currentSpecial.useDead && !Party.members[i-1].GetAlive()) {
			swap.interactable = false;
		} else {
			swap.interactable = true;
		}
	}
	
	public void Change () {
		Party.playerSlot = active;
	}
	
	public void UseItem () {
		item.UseOutOfCombat(active-1);
	}
	
	public int ConfirmCharacter () {
		return active - 1;
	}
	
	public void Replace () {
		Party.members[active - 1] = null;
		Party.playerCount--;
		Party.AddPlayer(Party.fullRecruit);
		Party.latestRecruit = Party.fullRecruit;
		Party.fullRecruit = null;
	}
	
	public void Kick () {
		Party.members[active - 1] = null;
		Party.playerCount--;
	}
	
	public void Special () {
	    if (Party.members[active - 1].GetSpecial().usableOut && !Party.members[active - 1].GetSupportSpecial().usableOut) {
			if (Party.members[active - 1].GetSpecial().selects) {
				dungeon.OpenSpecial(Party.members[active - 1].GetSupportSpecial());
				return;
			}
		    Party.members[active - 1].GetSpecial().UseOutOfCombat();
			CloseParty();
		} else if (!Party.members[active - 1].GetSpecial().usableOut && Party.members[active - 1].GetSupportSpecial().usableOut) {
			if (Party.members[active - 1].GetSupportSpecial().selects) {
				dungeon.OpenSpecial(Party.members[active - 1].GetSupportSpecial());
				return;
			}
			Party.members[active - 1].GetSupportSpecial().UseOutOfCombat();
			CloseParty();
		} else {
			specialMenu.gameObject.SetActive(true);
			StandardS.gameObject.SetActive(true);
			SupportS.gameObject.SetActive(true);
			StandardS.GetComponentInChildren<TextMeshProUGUI>().text = Party.members[active - 1].GetSpecial().GetName();
			SupportS.GetComponentInChildren<TextMeshProUGUI>().text = Party.members[active - 1].GetSupportSpecial().GetName();
		}
	}
	
	public void StandardSpecial () {
		Party.members[active - 1].GetSpecial().UseOutOfCombat();
		specialMenu.gameObject.SetActive(false);
		//StandardS.gameObject.SetActive(false);
		//SupportS.gameObject.SetActive(false);
		CloseParty();
	}
	
	public void SupportSpecial() {
		Party.members[active - 1].GetSupportSpecial().UseOutOfCombat();
		specialMenu.gameObject.SetActive(false);
		//StandardS.gameObject.SetActive(false);
		//SupportS.gameObject.SetActive(false);
		CloseParty();
	}
	
	public void RejectNewcomer() {
		Party.fullRecruit = null;
	}
	
	public void CloseParty() {
		gameObject.transform.parent.gameObject.GetComponent<Dungeon>().CloseParty();
	}
	
	public void CloseSpecialMenu() {
		specialMenu.gameObject.SetActive(false);
	}
}
