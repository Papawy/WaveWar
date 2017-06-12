using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBegining : Mission {

	// Use this for initialization
	protected override void Start()
	{
		//TargetNPC = GameObject.Find("Neighborhood_Rock/NPCs/NPC_1");
	}

	// Update is called once per frame
	protected override void Update()
	{
		/*if (TargetNPC.GetComponent<CharacterStats>().Dead)
		{
			IsCompleted = true;
			GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Vous avez réussi la mission !");
			GameObject.Find("HeroeTest").GetComponent<Animator>().SetTrigger("FistPump");
			GameObject.Destroy(this);
		}*/
	}

	public override void StartMission()
	{
		/*TargetNPC.GetComponentInChildren<Marker2D>().SetMarkerColor(Color.blue);
		TargetNPC.GetComponentInChildren<Marker2D>().ToggleMarker(true);
		GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Allez tuer " + TargetNPC.name);*/
	}
}
