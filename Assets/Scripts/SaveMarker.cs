using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMarker : MonoBehaviour {

	private uint coolDown = 3000;
	private uint saveTime = 0;

	private bool canSave = true;

	public int SaveLocation = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((uint)(Time.time * 1000) - saveTime > coolDown)
		{
			canSave = true;
			saveTime = 0;
		}
	}

	void OnTriggerEnter(Collider ColObj)
	{
		if (ColObj.gameObject.tag == "hero")
		{
			if (canSave)
			{
				saveTime = (uint)(Time.time * 1000);
				canSave = false;

				SaveManager.ActiveSave.Health = (int)GameObject.Find("HeroeTest").GetComponent<CharacterStats>().Life;
				SaveManager.ActiveSave.SaveLocation = this.SaveLocation;
				SaveManager.ActiveSave.Save();
				GameObject.Find("HeroeTest").GetComponent<TipsBox>().ShowTips("Partie sauvegardée !");
			}
		}
		//Physics.IgnoreCollision(ColObj.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
	}
}
