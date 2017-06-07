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
        switch(GlobalScript.Random.Next(3))
        {
            case 0:
                GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("NPC : J'ai pas de temps à perdre avec vous !");
                break;
            case 1:
                GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("NPC : J'ai pas de monnaie sur moi !");
                break;
            case 2:
                GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("NPC : Dégagez le chemin espèce de vil malandrin !");
                break;
        }
	}
}
