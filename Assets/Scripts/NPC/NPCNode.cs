using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNode : MonoBehaviour {

	public List<NPCNode> ConnectedNodes = new List<NPCNode>();

	// Use this for initialization
	void Start () {
		if (this.gameObject.GetComponent<MeshRenderer>() != null)
			this.gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Debug.isDebugBuild)
		{
			foreach (NPCNode node in ConnectedNodes)
			{
				Debug.DrawLine(this.gameObject.transform.position, node.gameObject.transform.position, Color.red);
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		if (Debug.isDebugBuild)
		{
			foreach (NPCNode node in ConnectedNodes)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(this.gameObject.transform.position, node.gameObject.transform.position);
			}
		}
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
