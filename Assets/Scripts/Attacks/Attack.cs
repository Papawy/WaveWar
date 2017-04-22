using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float Speed = 6.0f;
    public float MaxDistance = 15.0f;
    public float Damage = 20.0f;
	public uint LifeTime = 20000;

	public uint MaxBounces = 3;
    
    Vector3 initPos;
    Vector3 currPos;

	uint creationTime;

	// Use this for initialization
	void Start () {
        initPos = gameObject.transform.position;
		creationTime = (uint)(Time.time * 100);
	}
	
	// Update is called once per frame
	void Update () {
        currPos = gameObject.transform.position;
        if(Vector3.Distance(initPos, currPos) <= MaxDistance)
        {
            //gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
        }
		else if((uint)(Time.time*100) - creationTime > LifeTime)
		{
			GameObject.Destroy(this.gameObject);
		}
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision ColObj)
    {
        if (ColObj.gameObject.tag == "character")
        {
            ColObj.gameObject.GetComponent<CharacterStats>().RemoveHealth(Damage);
            GameObject.Destroy(this.gameObject);
        }
		if(MaxBounces == 0)
			GameObject.Destroy(this.gameObject);
		MaxBounces--;
		Damage = Damage / 2;
		//this.gameObject.transform.forward = Vector3.Reflect(this.gameObject.transform.position, ColObj.contacts[0].normal);
    }
}
