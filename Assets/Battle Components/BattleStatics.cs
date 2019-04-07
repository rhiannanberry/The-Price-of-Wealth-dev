using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BattleStatic {
    public static Battle instance;
    
    public static void AttachStatic(Battle b) {
        instance = b;
        SpecialStatic.AttachStatic();
    } 
}

public static class SpecialStatic {
    public static GameObject description;
    public static void AttachStatic() {
        description = BattleStatic.instance.specialMenu.transform.RecursiveFind("Description").gameObject;

    } 
}