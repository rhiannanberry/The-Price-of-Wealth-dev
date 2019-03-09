 #if UNITY_EDITOR


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TMProToggle : TMProGameObject
{

    
	[MenuItem("GameObject/UI/TextMeshPro - Toggle", false, 2033)]
	private static void CreateTMProToggle() {
		SetObjectParent(_Toggle());
	}
    private static Transform _Toggle() {
        GameObject go = DefaultControls.CreateToggle(resources);

        //bg
        go.transform.Find("Background").GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");

        go.transform.Find("Background/Checkmark").GetComponent<Image>().sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");


        //Label
        Object.DestroyImmediate(go.GetComponentInChildren<Text>());
        GameObject txt = go.transform.Find("Label").gameObject;

        Vector2 offsetMin = txt.GetComponent<RectTransform>().offsetMin;
        Vector2 offsetMax = txt.GetComponent<RectTransform>().offsetMax;

        txt.AddComponent<TextMeshProUGUI>();
        TextMeshProUGUI t = txt.GetComponent<TextMeshProUGUI>();
        
        t.text = "Toggle";
		t.color = new Color32(50,50,50,255);
		t.alignment = TextAlignmentOptions.TopLeft;
		t.fontSize = 14;

        txt.GetComponent<RectTransform>().offsetMin = offsetMin;
        txt.GetComponent<RectTransform>().offsetMax = offsetMax;

        return go.transform;
    }
}
 #endif