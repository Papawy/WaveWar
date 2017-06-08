using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public float Life = 100.0f;
	public float MaxLife = 100.0f;
	public bool Dead = false;

    public uint DeadTime = 5000;
    private uint deadTime = 0;
	private bool isDying = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isDying && (uint)(Time.time * 1000) - deadTime > DeadTime)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    void OnDeath()
    {
        deadTime = (uint)(Time.time * 1000);
		isDying = true;
    }

	public void RemoveHealth(float health)
	{
		if (Life - health > 0)
			Life -= health;
		else
		{
			Life = 0.0f;
			Dead = true;
            OnDeath();
		}
	}

	public void AddHealth(float health)
	{
		if (Life + health < MaxLife)
			Life += health;
		else
			Life = MaxLife;
	}
}
