using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPoint : InteractionBase {

	public GameObject OtherTP = null;

	private uint coolDown = 3000;
	private uint tpTime = 0;

	private bool canTP = true;

	// Use this for initialization
	void Start () {
		//Debug.Assert(OtherTP == null || OtherTP.GetComponent<TPPoint>() == null);
	}
	
	// Update is called once per frame
	void Update () {
		if ((uint)(Time.time * 1000) - tpTime > coolDown)
		{
			canTP = true;
			tpTime = 0;
		}
	}

	public override void OnInteract()
	{
		GameObject.Find("HeroeTest").transform.position = OtherTP.transform.position;
	}

	void OnTriggerEnter(Collider ColObj)
	{
		if (ColObj.gameObject.tag == "hero")
		{

			if(canTP)
			{
				GameObject.Find("HeroeTest").transform.position = new Vector3(OtherTP.transform.position.x, OtherTP.transform.position.y - 1.0f, OtherTP.transform.position.z);
				tpTime = (uint)(Time.time * 1000);
				OtherTP.GetComponent<TPPoint>().tpTime = (uint)(Time.time * 1000);
				canTP = false;
				OtherTP.GetComponent<TPPoint>().canTP = false;
			}
		}
		//Physics.IgnoreCollision(ColObj.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
	}
}
