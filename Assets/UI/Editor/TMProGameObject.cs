 #if UNITY_EDITOR
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TMProGameObject 
{
	  protected static DefaultControls.Resources resources = new DefaultControls.Resources();

    public static void SetObjectParent(Transform obj) {
		GameObject sel = Selection.activeGameObject;
		Canvas[] canvases = Object.FindObjectsOfType<Canvas>();
		bool canvasNeeded = false;

		Transform btnParent = null;
		if (sel == null) {
			canvasNeeded = canvases.Length == 0;
			btnParent = canvasNeeded ? CreateCanvas() : canvases[0].transform;
		} else {
			canvasNeeded = ((sel.GetComponent<Canvas>() == null) && (sel.GetComponentInParent<Canvas>() == null));
			btnParent = canvasNeeded ? CreateCanvas() : sel.transform;
			if (canvasNeeded) btnParent.SetParent(sel.transform);
		}


		obj.SetParent(btnParent);
		if (canvasNeeded) CreateEventSystem();

		obj.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


		CheckName(obj, btnParent);
	}

	private static bool EventSystemExists() {
		return (Object.FindObjectsOfType<EventSystem>() != null);
	}

	private static Transform CreateCanvas() {
		GameObject newCanvas = new GameObject("Canvas");
		newCanvas.AddComponent<Canvas>();
		newCanvas.AddComponent<CanvasScaler>();
		newCanvas.AddComponent<GraphicRaycaster>();
		newCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		return newCanvas.transform;	
	}

	private static void CreateEventSystem() {
		if (!EventSystemExists()) {
			GameObject e = new GameObject("EventSystem");
			e.AddComponent<EventSystem>();
			e.AddComponent<StandaloneInputModule>();
		}
	}

	private static void CheckName(Transform obj, Transform parent) {
		int i = 0;
		string name = obj.name;
		int nameIndex = 1;

		while (i < parent.childCount) {
			if (parent.GetChild(i).name == obj.name && parent.GetChild(i).GetInstanceID() != obj.GetInstanceID()) {
				i = 0;
				obj.name = name + " (" + nameIndex + ")";
				nameIndex++;
			} else {
				i++;
			}
		}
	}

	protected static GameObject _UIObject( string name ) {
		GameObject go = new GameObject(name);
		go.AddComponent<RectTransform>();
		go.AddComponent<CanvasRenderer>();
		return go;
	}
}
 #endif