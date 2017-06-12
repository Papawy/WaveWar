using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMarkerAnim : MonoBehaviour {

	public float UpDownRate = 3.0f;
	public float RotationRate = 250.0f;

	public float UpHeight = 1.0f;

	private bool up = true;
	private Vector3 originalPos;

	// Use this for initialization
	void Start () {
		originalPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.y > originalPos.y + UpHeight)
			up = false;
		else if (this.gameObject.transform.position.y < originalPos.y)
			up = true;

		if(up)
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y+0.2f*UpDownRate*Time.deltaTime, this.gameObject.transform.position.z);
		else
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.2f * UpDownRate * Time.deltaTime, this.gameObject.transform.position.z);

		this.gameObject.transform.Rotate(Vector3.forward, RotationRate * 1.0f * Time.deltaTime);
	}
}
