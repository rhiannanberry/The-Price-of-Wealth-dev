 #if UNITY_EDITOR


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TMProButton : TMProGameObject
{
	[MenuItem("GameObject/UI/TextMeshPro - Button", false, 2031)]
	private static void CreateTMProButton() {
		SetObjectParent(_Button());
	}


	private static Transform _Button() {
        GameObject go = DefaultControls.CreateButton(resources);

		go.GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        Object.DestroyImmediate(go.GetComponentInChildren<Text>());
        GameObject txt = go.transform.Find("Text").gameObject;
        txt.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI t = txt.GetComponent<TextMeshProUGUI>();
        
        t.text = "Button";
		t.color = new Color32(50,50,50,255);
		t.alignment = TextAlignmentOptions.Center;
		t.fontSize = 14;

        return go.transform;
    }
}
 #endif