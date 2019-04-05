using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public GameObject map;
	public GameObject spotlight;
	public static string selectedLocation;
	public static string currentPosition;
	public HelpMenu help;


	public Toggle tower, diningHall, researchLab, stadium, artCenter, healthBuilding, lectureHalls;

	// Use this for initialization
	void Start () {
		if (Areas.location == "overworld") {
			map.SetActive(true);
			
			spotlight.SetActive(false);
		} else {
			map.SetActive(false);
			spotlight.SetActive(true);
			spotlight.GetComponent<AreaSpotlight>().SetLocation(Areas.location);
		}
		if (!Areas.tutorialPlayed) {
			Areas.tutorialPlayed = true;
			help.Open();
		}
		map.transform.Find("Current Location").gameObject.GetComponent<Text>().text = "Current location: " + currentPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<ToggleGroup>().AnyTogglesOn()) {
			SetSelected(currentPosition);
		}
	}
	
	public void Select (string location) {
		selectedLocation = location;
	}

	public void SetSelected(string loc) {
		switch(loc) {
			case "tower":
				tower.isOn = true;
				break;
			case "dining":
				diningHall.isOn = true;
				break;
			case "research":
				researchLab.isOn = true;
				break;
			case "sports":
				stadium.isOn = true;
				break;
			case "art":
				artCenter.isOn = true;
				break;
			case "health":
				healthBuilding.isOn = true;
				break;
			case "lecture":
				lectureHalls.isOn = true;
				break;

		}
	}
}
