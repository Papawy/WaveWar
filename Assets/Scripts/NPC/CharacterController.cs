using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float Speed = 4.0f;

	Animator anim;

	Vector3 nextPos;
	bool moving;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		moving = false;
		nextPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
		{
			gameObject.transform.forward = nextPos - gameObject.transform.position;
			if(Mathf.Round(gameObject.transform.position.x) == Mathf.Round(nextPos.x) && Mathf.Round(gameObject.transform.position.y) == Mathf.Round(nextPos.y))
			{
				moving = false;
				anim.SetFloat("Speed", 0.0f);
			}
			else
			{
				anim.SetFloat("Speed", 1.0f);
				this.transform.position += transform.forward * Speed * 1.0f * Time.deltaTime;
			}

		}
	}

	public void MoveTo(Vector3 position)
	{
		nextPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
		Debug.Log("nextPos : " + nextPos.x + ";" + nextPos.y + ";" + nextPos.z);
		moving = true;
	}
}
