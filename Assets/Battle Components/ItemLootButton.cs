using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemLootButton : MonoBehaviour
{
    public Item item;
    public bool inBag = false;
    [HideInInspector]
    public Transform bagContainer;
    public Transform lootContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleLocation(){
        if(!inBag && bagContainer.childCount < 10) {
            transform.SetParent(bagContainer);
            inBag = true;
        } else if (inBag) {
            transform.SetParent(lootContainer);
            inBag = false;
        }
    }
}
