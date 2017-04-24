using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

	public enum TYPE
	{
		NORMAL,
		PRECISION,
		LARGE
	}

	public TYPE CurrentAttackType = TYPE.NORMAL;

	public GameObject[] AttackNotes = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LaunchAttack()
	{
		switch(CurrentAttackType)
		{
			case TYPE.NORMAL:
				{
					System.Random rnd = GlobalScript.Random;
					GameObject attackNote = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote.transform.rotation = Camera.main.transform.rotation;
					attackNote.transform.position = gameObject.transform.position + Vector3.up;
					attackNote.transform.position += gameObject.transform.forward;
					attackNote.GetComponent<Rigidbody>().velocity = attackNote.transform.forward * 15;
					break;
				}
			case TYPE.PRECISION:
				{
					System.Random rnd = GlobalScript.Random;
					GameObject attackNote = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote.transform.rotation = Camera.main.transform.rotation;
					attackNote.transform.position = gameObject.transform.position + Vector3.up;
					attackNote.transform.position += gameObject.transform.forward;
					attackNote.GetComponent<Rigidbody>().velocity = attackNote.transform.forward * 30;
					break;
				}
			case TYPE.LARGE:
				{
					System.Random rnd = GlobalScript.Random;
					GameObject attackNote = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote.transform.rotation = Camera.main.transform.rotation;
					attackNote.transform.position = gameObject.transform.position + Vector3.up;
					attackNote.transform.position += gameObject.transform.forward;
					attackNote.GetComponent<Rigidbody>().velocity = attackNote.transform.forward * 15;

					GameObject attackNote2 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote2.transform.rotation = Camera.main.transform.rotation;
					attackNote2.transform.position = gameObject.transform.position + Vector3.up + gameObject.transform.right * 0.2f; ;
					attackNote2.transform.position += gameObject.transform.forward;
					attackNote2.GetComponent<Rigidbody>().velocity = attackNote2.transform.forward * 15 - attackNote2.transform.right * 5; ;

					GameObject attackNote3 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote3.transform.rotation = Camera.main.transform.rotation;
					attackNote3.transform.position = gameObject.transform.position + Vector3.up - gameObject.transform.right * 0.2f; ;
					attackNote3.transform.position += gameObject.transform.forward;
					attackNote3.GetComponent<Rigidbody>().velocity = attackNote3.transform.forward * 15 + attackNote3.transform.right * 5;
					break;
				}
		}
	}
}
