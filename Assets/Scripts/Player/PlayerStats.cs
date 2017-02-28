using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	public UnityEngine.UI.Text LifeText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		LifeText.text = "Vie : " + Mathf.Round(Life);
	}
}
