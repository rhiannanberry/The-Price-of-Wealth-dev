﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour {
    
	public GameObject start;
	public GameObject selectCharacter;
	public GameObject enterName;
	public TMP_InputField nameDisplay;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Reset() {
		System.Random rng = new System.Random();
		int seed = rng.Next(10);
		Load(seed);
	}
	
	public void Load(int seed) {
		Party.Clear();
		Character c;
		if (seed == 0) {
		    c = new CSMajor();
		    c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(30); c.SetHealth(30); c.SetQuirk(new Ninja(c));
		    Party.AddPlayer(c);
		    Party.SetItems(new Item[] {new Pizza(), new Smartphone(), new Textbook(), new Pizza(), new Wire(), null, null, null, null, null});
		} else if (seed == 1) {
			c = new ChemistryMajor();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(30); c.SetHealth(30); c.SetQuirk(new Ill(c));
			Party.AddPlayer(c);
			Party.PostBattle();
			Party.SetItems(new Item[] {new Flask(), new Flask(), new ToxicSolution(), new MysteryGoo(), new MysteryGoo(), null, null, null, null, null});
		} else if (seed == 2) {
			c = new MusicMajor();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(30); c.SetHealth(30); c.SetQuirk(new Bandwagon(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Tuba(), new Smartphone(), new Donut(), new Wire(), new Heels(), null, null, null, null, null});
		} else if (seed == 3) {
			c = new HistoryMajor();
			c.SetChampion(true); c.SetStrength(5); c.SetMaxHP(35); c.SetHealth(35); c.SetQuirk(new Vaccinated(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Sword(), new Sword(), new Textbook(), new Curry(), new Celery(), null, null, null, null, null});
		} else if (seed == 4) {
			c = new CJMajor();
			c.SetChampion(true); c.SetStrength(5); c.SetMaxHP(30); c.SetHealth(30); c.SetBaseAccuracy(15); c.SetQuirk(new Vengeful(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Sword(), new Tazer(), new ProteinBar(), new Whistle(), new SlimeGoo(), null, null, null, null, null});
		} else if (seed == 5) {
			c = new PreMed();
			c.SetChampion(true); c.SetStrength(5); c.SetMaxHP(25); c.SetHealth(25); c.SetQuirk(new Recovery(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Sanitizer(), new Antibiotics(), new Rice(), new MysterySolution(), new ToxicSolution(), null, null, null, null, null});
		} else if (seed == 6) {
			c = new EnglishMajor();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(30); c.SetHealth(30); c.SetQuirk(new SleepDeprived(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Pencil(), new VotedBadge(), new Textbook(), new Pendulum(), new Celery(), null, null, null, null, null});
		} else if (seed == 7) {
			c = new BusinessMajor();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(35); c.SetHealth(35); c.SetQuirk(new Paranoid(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Briefcase(), new Smartphone(), new PinkSlip(), new Pizza(), new Pendulum(), null, null, null, null, null});
		} else if (seed == 8) {
			c = new PsychMajor();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(35); c.SetHealth(35); c.SetQuirk(new Dodgy(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Pendulum(), new Tuba(), new Textbook(), new Rice(), new Milk(), null, null, null, null, null});
		} else {
			c = new MechanicalEngineer();
			c.SetChampion(true); c.SetStrength(4); c.SetMaxHP(35); c.SetHealth(35); c.SetQuirk(new Overconfident(c));
			Party.AddPlayer(c);
			Party.SetItems(new Item[] {new Smartphone(), new Calculator(), new Pizza(), new Oil(), new Shuttle(), null, null, null, null, null});
		}
		ToName();
	}
	
	public void ToSelect () {
		selectCharacter.SetActive(true);
		start.SetActive(false);
	}
	
	public void ToName () {
		nameDisplay.text = Party.GetPlayer().GetName();
		selectCharacter.SetActive(false);
		enterName.SetActive(true);
	}
	
	public void Name () {
		Party.GetPlayer().SetName(nameDisplay.text);
	}
	
	public void CreateGame () {
		Areas.Initialize();
		SceneManager.LoadScene("Overworld");
	}
	
	public void End () {
		SceneManager.LoadScene("Start");
	}
}
