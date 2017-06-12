using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : InteractionBase {

    public Mission mission = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(mission != null && mission.IsCompleted)
        {
            GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Vous avez réussi la mission !");
            GameObject.Destroy(this.gameObject);
        }
            
    }

    public void LaunchMission()
    {
		mission.StartMission();
    }

	public void LaunchMission(Mission t_mission)
	{
		mission = t_mission;
		mission.StartMission();
	}

	public override void OnInteract()
    {
        LaunchMission();
    }
}
