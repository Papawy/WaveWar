using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : InteractionBase {

    public ObjectiveKillNPC objective = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(objective != null && objective.IsCompleted)
        {
            GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Vous avez réussi la mission !");
            GameObject.Destroy(this.gameObject);
        }
            
    }

    public void LaunchMission()
    {
        objective = this.gameObject.AddComponent<ObjectiveKillNPC>();
        objective.TargetNPC = GameObject.Find("NPCs/NPC_1");
        objective.StartObjective();
    }

    public override void OnInteract()
    {
        LaunchMission();
    }
}
