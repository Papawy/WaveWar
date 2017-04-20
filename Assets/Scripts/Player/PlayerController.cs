﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public float Speed = 4.0f;
    public GameObject[] AttackNotes = null;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float move = (Mathf.Abs(TeamUtility.IO.InputManager.GetAxisRaw("Vertical")) + Mathf.Abs(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal")));
		move = Mathf.Clamp(move, 0.0f, 0.5f);
		Quaternion camRot = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y + Mathf.Atan(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal")/(TeamUtility.IO.InputManager.GetAxisRaw("Vertical")+0.0001f))*(180/Mathf.PI), 0);

		if (TeamUtility.IO.InputManager.GetAxisRaw("Vertical") < 0)
		{
			this.transform.rotation = Quaternion.Euler(0.0f, camRot.eulerAngles.y + (180*Mathf.Sign(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal")*-1)), 0);
			//this.transform.position += transform.forward * Speed * move * Time.deltaTime;
		}
		else if(move != 0.0f)
		{
			this.transform.rotation = camRot;
			//this.transform.position += transform.forward * Speed * move * Time.deltaTime;
		}

		if(TeamUtility.IO.InputManager.GetButtonDown("Accept"))
		{
			GameObject.Find("NPC_1").GetComponent<CharacterController>().MoveTo(this.transform.position);
		}
		if (TeamUtility.IO.InputManager.GetButtonDown("Cancel"))
		{
			GameObject.Find("NPC_1").GetComponent<CharacterController>().MoveToNode();
		}

		if (TeamUtility.IO.InputAdapter.GetButton("Aim"))
		{
			this.transform.rotation = camRot;
			anim.SetBool("Guitar", true);
			if (TeamUtility.IO.InputManager.GetButtonDown("Attack"))
			{
				System.Random rnd = new System.Random();
				GameObject attackNote = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length-1)]);
				attackNote.transform.rotation = Camera.main.transform.rotation;
				attackNote.transform.position = gameObject.transform.position + Vector3.up;
				attackNote.transform.position += gameObject.transform.forward;
			}
		}
		else
			anim.SetBool("Guitar", false);

		if (TeamUtility.IO.InputAdapter.GetButton("Sprint"))
			move = move * 2;

		anim.SetFloat("Speed", move);
	}
}
