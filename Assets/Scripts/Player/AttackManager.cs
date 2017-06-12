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

	public void LaunchAttack(float mult = 1.0f)
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
					attackNote.GetComponent<Attack>().Damage = 15.0f*mult;

					GameObject attackNote2 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote2.transform.rotation = Camera.main.transform.rotation;
					attackNote2.transform.position = gameObject.transform.position + Vector3.up + gameObject.transform.right * 0.2f; ;
					attackNote2.transform.position += gameObject.transform.forward;
					attackNote2.GetComponent<Rigidbody>().velocity = attackNote2.transform.forward * 15 - attackNote2.transform.right * 3;
					attackNote2.GetComponent<Attack>().Damage = 15.0f * mult;

					GameObject attackNote3 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote3.transform.rotation = Camera.main.transform.rotation;
					attackNote3.transform.position = gameObject.transform.position + Vector3.up - gameObject.transform.right * 0.2f; ;
					attackNote3.transform.position += gameObject.transform.forward;
					attackNote3.GetComponent<Rigidbody>().velocity = attackNote3.transform.forward * 15 + attackNote3.transform.right * 3;
					attackNote3.GetComponent<Attack>().Damage = 15.0f * mult;
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
					attackNote.GetComponent<Attack>().Damage = 10.0f * mult;

					GameObject attackNote2 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote2.transform.rotation = Camera.main.transform.rotation;
					attackNote2.transform.position = gameObject.transform.position + Vector3.up + gameObject.transform.right * 0.2f;
					attackNote2.transform.position += gameObject.transform.forward;
					attackNote2.GetComponent<Rigidbody>().velocity = attackNote2.transform.forward * 15 - attackNote2.transform.right * 3;
					attackNote2.GetComponent<Attack>().Damage = 10.0f * mult;

					GameObject attackNote3 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote3.transform.rotation = Camera.main.transform.rotation;
					attackNote3.transform.position = gameObject.transform.position + Vector3.up - gameObject.transform.right * 0.2f;
					attackNote3.transform.position += gameObject.transform.forward;
					attackNote3.GetComponent<Rigidbody>().velocity = attackNote3.transform.forward * 15 + attackNote3.transform.right * 3;
					attackNote3.GetComponent<Attack>().Damage = 10.0f * mult;

					GameObject attackNote4 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote4.transform.rotation = Camera.main.transform.rotation;
					attackNote4.transform.position = gameObject.transform.position + Vector3.up + gameObject.transform.right * 0.4f;
					attackNote4.transform.position += gameObject.transform.forward;
					attackNote4.GetComponent<Rigidbody>().velocity = attackNote4.transform.forward * 15 - attackNote4.transform.right * 6;
					attackNote4.GetComponent<Attack>().Damage = 10.0f * mult;

					GameObject attackNote5 = GameObject.Instantiate(AttackNotes[rnd.Next(AttackNotes.Length)]);
					attackNote5.transform.rotation = Camera.main.transform.rotation;
					attackNote5.transform.position = gameObject.transform.position + Vector3.up - gameObject.transform.right * 0.4f;
					attackNote5.transform.position += gameObject.transform.forward;
					attackNote5.GetComponent<Rigidbody>().velocity = attackNote5.transform.forward * 15 + attackNote5.transform.right * 6;
					attackNote5.GetComponent<Attack>().Damage = 10.0f * mult;

					break;
				}
		}
	}
}
