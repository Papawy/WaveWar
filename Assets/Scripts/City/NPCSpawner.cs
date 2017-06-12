using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

	public List<GameObject> Neighborhoods;
	public List<GameObject> NPCPrefabs;
	public uint SpawningRate = 2000;

	public int NPCLimit = 50;

	public int SpawnedNPC = 0;

	private uint lastSpawned = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((uint)(Time.time * 1000) - lastSpawned > SpawningRate)
		{
			if(SpawnedNPC <= NPCLimit)
			{
				System.Random rnd = GlobalScript.Random;
				GameObject npc = GameObject.Instantiate(NPCPrefabs[rnd.Next(NPCPrefabs.Count)]);
				GameObject nodesList = Neighborhoods[rnd.Next(Neighborhoods.Count)].transform.FindChild("NPC_Nodes").gameObject;
				GameObject node = nodesList.transform.GetChild(rnd.Next(nodesList.transform.childCount)).gameObject;
				npc.transform.position = new Vector3(node.transform.position.x, node.transform.position.y - 1.0f, node.transform.position.z);
				npc.GetComponent<CharacterController>().MoveToNode(node.GetComponent<NPCNode>());
				SpawnedNPC += 1;
			}
			lastSpawned = (uint)(Time.time * 1000);
		}
	}
}
