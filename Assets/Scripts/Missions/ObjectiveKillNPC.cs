using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveKillNPC : MonoBehaviour {

    public GameObject TargetNPC = null;

    public bool IsCompleted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TargetNPC.GetComponent<CharacterStats>().Dead)
            IsCompleted = true;
	}

    public void StartObjective()
    {
        TargetNPC.GetComponentInChildren<Marker2D>().SetMarkerColor(Color.blue);
        TargetNPC.GetComponentInChildren<Marker2D>().ToggleMarker(true);
        GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Allez tuer "+TargetNPC.name);
    }
}
