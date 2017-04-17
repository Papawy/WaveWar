using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNode : MonoBehaviour {

	public List<NPCNode> ConnectedNodes = new List<NPCNode>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool ConnectTo(NPCNode other)
	{
		if(!ConnectedNodes.Contains(other))
		{
			this.ConnectedNodes.Add(other);
			if (!other.ConnectedNodes.Contains(this))
				other.ConnectedNodes.Add(this);
			return true;
		}
		return false;
	}
}
