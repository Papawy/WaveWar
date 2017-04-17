using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float Speed = 4.0f;
	public BEHAVIOR Behavior = BEHAVIOR.NEUTRAL;

	Animator anim;

	Vector3 nextPos;
	bool moving;

	public NPCNode CurrentNode = null;

	public enum BEHAVIOR
	{
		FOLLOWING_NODES, // Follow random nodes
		FEAR, // Avoid the area of designated NPC/Player
		AGGRESSIVE, // Move toward a designated NPC or Player and try to attack him
		NEUTRAL // Won't move
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		moving = false;
		nextPos = gameObject.transform.position;
		if (Behavior == BEHAVIOR.FOLLOWING_NODES && CurrentNode != null)
			MoveToNode();
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
				if(Behavior == BEHAVIOR.FOLLOWING_NODES)
				{
					System.Random rnd = new System.Random();
					NPCNode prev = CurrentNode;
					NPCNode tmp = prev;
					while (tmp == prev)
						tmp = CurrentNode.ConnectedNodes[rnd.Next(CurrentNode.ConnectedNodes.Count)];
					CurrentNode = tmp;

					Debug.Log(CurrentNode.ToString());
					MoveToNode();
				}
				Debug.Log("Stoping movement");
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
		moving = true;
		Behavior = BEHAVIOR.NEUTRAL;
	}

	public void MoveToNode(NPCNode node)
	{
		MoveTo(new Vector3(node.transform.position.x, gameObject.transform.position.y, node.transform.position.z));
		Behavior = BEHAVIOR.FOLLOWING_NODES;
		CurrentNode = node;
	}

	public void MoveToNode()
	{
		if(CurrentNode != null)
		{
			MoveToNode(CurrentNode);
		}
	}
}
