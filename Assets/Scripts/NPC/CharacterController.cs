using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float Speed = 4.0f;
	public BEHAVIOR Behavior = BEHAVIOR.NEUTRAL;
	public DEPLACEMENT DeplacementStyle = DEPLACEMENT.STANDING;

	Animator anim;

	Vector3 nextPos;
	bool moving;

	public GameObject MarkerText = null;

	public NPCNode CurrentNode = null;

    public uint RaycastUpdateTime = 250;
	public float RaycastDistance = 3.0f;
    private uint lastRaycast = 0;
	private bool avoidingProc = false;
	private int avoidingDir = 0;

	public enum BEHAVIOR
	{
		FOLLOWING_NODES, // Follow random nodes
		FEAR, // Avoid the area of designated NPC/Player
		AGGRESSIVE, // Move toward a designated NPC or Player and try to attack him
		NEUTRAL // Won't move
	}

	public enum DEPLACEMENT
	{
		STANDING,
		WALKING,
		RUNNING,
		CROUCHING
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

		if(MarkerText != null)
			MarkerText.transform.LookAt(Camera.main.transform);

		if(avoidingProc == false)
			gameObject.transform.forward = nextPos - gameObject.transform.position;

		if (moving)
		{
			if(Mathf.Round(gameObject.transform.position.x) == Mathf.Round(nextPos.x) && Mathf.Round(gameObject.transform.position.z) == Mathf.Round(nextPos.z))
			{
				moving = false;
				DeplacementStyle = DEPLACEMENT.STANDING;
				anim.SetFloat("Speed", 0.0f);
				if(Behavior == BEHAVIOR.FOLLOWING_NODES)
				{
					System.Random rnd = GlobalScript.Random;
					NPCNode tmp = CurrentNode;
					if (CurrentNode.ConnectedNodes.Count != 0)
					{
						while (tmp == CurrentNode)
							tmp = CurrentNode.ConnectedNodes[rnd.Next(CurrentNode.ConnectedNodes.Count)];
						CurrentNode = tmp;

						MoveToNode();
					}
					else
						Behavior = BEHAVIOR.NEUTRAL;

				}
			}
			else
			{
				switch(DeplacementStyle)
				{
					case DEPLACEMENT.STANDING:
						//moving = false;
						anim.SetFloat("Speed", 0.0f);
						break;
					case DEPLACEMENT.WALKING:
						anim.SetFloat("Speed", 0.5f);
						break;
					case DEPLACEMENT.RUNNING:
						anim.SetFloat("Speed", 1.0f);
						break;
					case DEPLACEMENT.CROUCHING:
						break;
				}
				if ((uint)(Time.time * 1000) - lastRaycast >= RaycastUpdateTime)
				{
					Vector3 positon = gameObject.transform.position + Vector3.up / 2;
					if (avoidingProc)
					{
						switch(avoidingDir)
						{
							case 1:
								if (Physics.Raycast(positon, (gameObject.transform.forward - gameObject.transform.right) / 2, RaycastDistance))
								{
									gameObject.transform.forward = (gameObject.transform.forward + gameObject.transform.right) / 2;
									avoidingDir = 1;
								}
								else
								{
									gameObject.transform.forward = nextPos - gameObject.transform.position;
									avoidingProc = false;
									avoidingDir = 0;
								}
								break;
							case 2:
								if (Physics.Raycast(positon, -gameObject.transform.right, RaycastDistance))
								{
									gameObject.transform.forward = gameObject.transform.right;
									avoidingDir = 2;
								}
								else
								{
									gameObject.transform.forward = nextPos - gameObject.transform.position;
									avoidingProc = false;
									avoidingDir = 0;
								}
								break;
							case -1:
								if (Physics.Raycast(positon, (gameObject.transform.forward + gameObject.transform.right) / 2, RaycastDistance))
								{
									gameObject.transform.forward = (gameObject.transform.forward - gameObject.transform.right) / 2;
									avoidingDir = -1;
								}
								else
								{
									gameObject.transform.forward = nextPos - gameObject.transform.position;
									avoidingProc = false;
									avoidingDir = 0;
								}
								break;
							case -2:
								if (Physics.Raycast(positon, gameObject.transform.right, RaycastDistance))
								{
									gameObject.transform.forward = -gameObject.transform.right;
									avoidingDir = -2;
								}
								else
								{
									gameObject.transform.forward = nextPos - gameObject.transform.position;
									avoidingProc = false;
									avoidingDir = 0;
								}
								break;
							default:

								break;
						}
					}
					else
					{
						if (Physics.Raycast(positon, gameObject.transform.forward, RaycastDistance) ||
						Physics.Raycast(positon, (gameObject.transform.forward * 4 + gameObject.transform.right) / 5, RaycastDistance) ||
						Physics.Raycast(positon, (gameObject.transform.forward * 4 - gameObject.transform.right) / 5, RaycastDistance))
						{
							if (Physics.Raycast(positon, (gameObject.transform.forward + gameObject.transform.right) / 2, RaycastDistance))
							{
								if (Physics.Raycast(positon, gameObject.transform.right, RaycastDistance))
								{
									if (Physics.Raycast(positon, (gameObject.transform.forward - gameObject.transform.right) / 2, RaycastDistance))
									{
										if (Physics.Raycast(positon, -gameObject.transform.right, RaycastDistance))
										{
										}
										else
										{
											gameObject.transform.forward = -gameObject.transform.right;
											avoidingDir = -2;
										}
									}
									else
									{
										gameObject.transform.forward = (gameObject.transform.forward - gameObject.transform.right) / 2;
										avoidingDir = -1;
									}
								}
								else
								{
									gameObject.transform.forward = gameObject.transform.right;
									avoidingDir = 2;
								}
							}
							else
							{
								gameObject.transform.forward = (gameObject.transform.forward + gameObject.transform.right) / 2;
								avoidingDir = 1;
							}
							avoidingProc = true;
						}
						else
						{
							gameObject.transform.forward = nextPos - gameObject.transform.position;
							avoidingProc = false;
							avoidingDir = 0;
						}
					}
					lastRaycast = (uint)(Time.time * 1000);
				}
				//this.transform.position += transform.forward * Speed * 1.0f * Time.deltaTime;
			}

		}
	}

	public void MoveTo(Vector3 position)
	{
		nextPos = new Vector3(position.x, gameObject.transform.position.y, position.z);
		moving = true;
		Behavior = BEHAVIOR.NEUTRAL;
		DeplacementStyle = DEPLACEMENT.WALKING;
	}

	public void MoveToNode(NPCNode node)
	{
		MoveTo(new Vector3(node.transform.position.x, gameObject.transform.position.y, node.transform.position.z));
		Behavior = BEHAVIOR.FOLLOWING_NODES;
		DeplacementStyle = DEPLACEMENT.WALKING;
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
