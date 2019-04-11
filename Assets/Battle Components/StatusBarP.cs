﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarP : MonoBehaviour {

	public TextMeshProUGUI message;
	string charName;
	string health;
	string sp;
	string offense;
	string defense;
	string accEvade;
	string quirk;
	string statuses;
	Character player;
	public bool frozen;
	
	// Use this for initialization
	void Start () {
		message.text = "";
		Check();
	}
	
	// Update is called once per frame
	void Update () {
		//Check();
	}
	
	public void Check () {
		try {
		    player = Party.GetPlayer();
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.GetName();
		    charName = player.GetName() + " (" + player.type + ")";
	     	health = "HP: " + player.GetHealth().ToString() + "/" + player.GetMaxHP().ToString();
			sp = ", SP: " + Party.GetSP().ToString() + "/40";
			offense = "Power: " + player.GetPower().ToString() + "    Charge: " + player.GetCharge().ToString();
			defense = "Defense: " + player.GetDefense().ToString() + "    Guard: " + player.GetGuard().ToString();
		    accEvade = "Acc: " + player.GetAccuracy().ToString() + "    Evade: " + player.GetEvasion().ToString();
		    quirk = "Quirk: " + player.GetQuirk().GetName();
			statuses = player.status.BarText();
		    message.text = charName + "\n" + health + sp + "\n" + offense + "\n" + defense + "\n" + accEvade + "\n" + quirk + "\n" + statuses;
		} catch (System.NullReferenceException e) {
			message.text = "";
		}
	}
}
