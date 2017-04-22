using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNPCInteraction : InteractionBase {

	AudioSource m_audioSource = null;

	// Use this for initialization
	void Start () {
		if(this.gameObject.GetComponent<AudioSource>() == null)
		{
			m_audioSource = this.gameObject.AddComponent<AudioSource>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnInteract()
	{
		Debug.Log("Beep Boop");
	}
}
