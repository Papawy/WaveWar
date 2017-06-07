using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

	InteractionBase m_interaction = null;

	public TYPE InteractionType = TYPE.NONE;

	public enum TYPE
	{
		NONE,
		NPC_CITY,
        MISSION
	}

	// Use this for initialization
	void Start () {
		switch(InteractionType)
		{
			case TYPE.NONE:
				break;
			case TYPE.NPC_CITY:
				m_interaction = this.gameObject.AddComponent<CityNPCInteraction>();
				break;
            case TYPE.MISSION:
                m_interaction = this.gameObject.AddComponent<MissionManager>();
                break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Interact()
	{
		if(m_interaction != null)
			m_interaction.OnInteract();
	}
}
