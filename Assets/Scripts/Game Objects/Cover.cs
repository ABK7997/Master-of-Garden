using UnityEngine;
using System.Collections;

public class Cover : Item {

    public static bool purchased = false;
	
	// Update is called once per frame
	void Update () {
	    if (purchased) text.text = "";
	}
}
