using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatSelfGeneration : MonoBehaviour {

	public int Height;
	public float ScaleFactor;
	public List<GameObject> Grounds;
	public List<GameObject> Floors;
	public List<GameObject> Rooftops;

	private List<GameObject> m_building;

	// Use this for initialization
	void Start () {

		m_building = new List<GameObject>();
		Generate(Height);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Generate(int height)
	{
		m_building.Clear();

		System.Random rnd = new System.Random();

		m_building.Add((GameObject)Instantiate(Grounds[rnd.Next(0, Grounds.Count)], gameObject.transform.position, gameObject.transform.rotation));
		Debug.Log(m_building[0].GetComponentInChildren<Renderer>().bounds.size.y);
		int idx = 0;
		for (int i = 1; i <= Height; i++)
		{
			idx = rnd.Next(0, Floors.Count);
			
			m_building.Add((GameObject)Instantiate(Floors[idx], gameObject.transform.position + (Vector3.up * m_building[i-1].GetComponentInChildren<Renderer>().bounds.size.y * i), gameObject.transform.rotation));
			Debug.Log(m_building[i].GetComponentInChildren<Renderer>().bounds.size.y);
		}
		idx = rnd.Next(0, Rooftops.Count);
		m_building.Add((GameObject)Instantiate(Rooftops[idx], gameObject.transform.position + Vector3.up * m_building[m_building.Count - 1].GetComponentInChildren<Renderer>().bounds.size.y * (Height + 1), gameObject.transform.rotation));
		foreach (GameObject floor in m_building)
		{
			floor.transform.parent = this.gameObject.transform;
		}

		this.gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
}
