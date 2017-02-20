using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public float Speed = 1.0f;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float move = TeamUtility.IO.InputManager.GetAxisRaw("Vertical");
		anim.SetFloat("Speed", move);
		Quaternion camRot = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0);
		if(move != 0.0)
		{
			this.transform.rotation = camRot;
		}
		if (TeamUtility.IO.InputManager.GetAxisRaw("Vertical") > 0)
		{
			this.transform.rotation = camRot;
			//this.transform.position += transform.forward * Speed * Time.deltaTime;
		}
		else if (TeamUtility.IO.InputManager.GetAxisRaw("Vertical") < 0)
		{
			this.transform.rotation = camRot;
			//this.transform.position -= transform.forward * Speed * Time.deltaTime;
		}

		if (TeamUtility.IO.InputManager.GetAxisRaw("Horizontal") < 0)
		{
			this.transform.rotation = camRot;
			//this.transform.position -= transform.right * Speed * Time.deltaTime;
		}
		else if (TeamUtility.IO.InputManager.GetAxisRaw("Horizontal") > 0)
		{
			this.transform.rotation = camRot;
			//this.transform.position += transform.right * Speed * Time.deltaTime;
		}

	}
}
