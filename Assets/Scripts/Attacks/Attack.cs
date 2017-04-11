using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float Speed = 6.0f;
    public float MaxDistance = 15.0f;
    public float Damage = 20.0f;
    
    Vector3 initPos;
    Vector3 currPos;

	// Use this for initialization
	void Start () {
        initPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        currPos = gameObject.transform.position;
        if(Vector3.Distance(initPos, currPos) <= MaxDistance)
        {
            gameObject.transform.position += gameObject.transform.forward * Speed * Time.deltaTime;
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
    }
}
