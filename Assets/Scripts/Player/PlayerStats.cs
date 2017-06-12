using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	public UnityEngine.UI.Slider LifeBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LifeBar.value = Life;
		/*if(Life <= 0)
		{
			
		}*/
	}

	protected override void OnDeath()
	{
		Dead = true;
		this.gameObject.GetComponent<Animator>().SetBool("Dead", true);
	}
}
