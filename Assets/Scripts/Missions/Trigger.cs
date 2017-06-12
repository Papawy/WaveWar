using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	public Mission MissionToStart;

	public MISSION_TYPE MissionType;

	public enum MISSION_TYPE
	{
		KILL_NPC,
		GET_OBJ
	}

	// Use this for initialization
	void Start () {
		switch (MissionType)
		{
			case MISSION_TYPE.KILL_NPC:
				MissionToStart = GameObject.Find("HeroeTest").AddComponent<MissionKillNPC>();
				break;
			case MISSION_TYPE.GET_OBJ:
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision ColObj)
	{
		if (ColObj.gameObject.tag == "hero")
		{
			MissionToStart.StartMission();
			//this.gameObject.AddComponent<MissionManager>().LaunchMission(MissionToStart);
			Destroy(this.gameObject);
		}
		Physics.IgnoreCollision(ColObj.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>(), true);
	}
}
