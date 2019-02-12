using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapViewer : MonoBehaviour {
	
	public DungeonMap graph;
	public Dungeon eventCatcher;
	//public Dictionary<string, Event> eventMap;
	public string location;
	public Event current;
	
	void Start () {
		UpdateMap();
	}
	
	void OnEnable() {
		if (graph != null) {
			//Debug.Log("here");
		    UpdateMap();
		} else {
			CallGraph();
		}
	}
	
	void CallGraph() {
		graph = DungeonMapData.data[location];
		//eventMap = DungeonMapData.lectureEvents;
		//eventCatcher = gameObject.transform.parent.parent.GetComponent<Dungeon>();
		graph.unityView = this;
	}
	
	public bool IsCleared(string node) {
	    //return false;
		return gameObject.transform.Find(node).gameObject.GetComponent<RoomNode>().cleared;
	}
	
	public void UpdateMap () {
		foreach (Transform c in gameObject.transform) {
			c.Find("Button").gameObject.GetComponent<Button>().interactable = false;
		}
		List<string> available = graph.GetReachable();
		foreach (string s in available) {
			if (graph.cleared[s]) {
                gameObject.transform.Find(s).Find("Image").gameObject.GetComponent<Image>().color = Color.green;
			} else {
				gameObject.transform.Find(s).Find("Image").gameObject.GetComponent<Image>().color = Color.red;
			}
			gameObject.transform.Find(graph.position).Find("Image").gameObject.GetComponent<Image>().color = Color.yellow;
			gameObject.transform.Find(s).Find("Button").gameObject.GetComponent<Button>().interactable = true;
		}
	}
	
	public void RunEvent(string id) {
		//Event e = eventMap[id];
		current = graph.eventMap[id];
		if (current != null) {
			if (id.Contains("Boss")) {
				Areas.cleared[location] = true;
			}
		    eventCatcher.RunEvent(current);
		    //Temporary solution
		    graph.eventMap[id] = null;
		} else {
			UpdateMap();
		}
	}
	
	public void Escape() {
		graph.eventMap[graph.position] = current;
		graph.cleared[graph.position] = false;
		if (graph.position.Contains("Boss")) {
			Areas.cleared[location] = false;
		}
		graph.position = graph.previousPosition;
		UpdateMap();
	}
	
	public void EscapeEnemies(Character[] enemies) {
		foreach (Character c in enemies) {
			if (c != null) {
				if (c.GetAlive()) {
					c.PostBattle();
					c.Heal(100);
					c.drops = new Item[0];
				}
			}
		}
		if (graph == null) {
			CallGraph();
		}
		//Debug.Log(graph.position);
		graph.eventMap[graph.position] = new BattleEvent(enemies, "Another Try");
		graph.cleared[graph.position] = false;
		if (graph.position.Contains("Boss")) {
			Areas.cleared[location] = false;
			graph.eventMap[graph.position] = current;
		}
		graph.position = graph.previousPosition;
		UpdateMap();
	}
	
}