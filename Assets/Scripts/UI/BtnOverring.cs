using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnOverring : MonoBehaviour {

	public float Distance = 0.7f;
	public float Speed = 5.0f;

	public Color SelectColor = new Color(184, 42, 42);

	public bool ActiveMouse = false;

	private short m_isMoving = 0;
	private Vector3 m_basePos;
	private bool m_oldHoverState = false;
	private bool m_hoverState = false;

	private Color m_originalColor;

	public void OnPointerEnter()
	{
		m_hoverState = true;
	}

	public void OnPointerExit()
	{
		m_hoverState = false;
	}

	public void OnSelected()
	{
		if(!ActiveMouse)
			m_isMoving = 1;
	}

	public void OnDeselected()
	{
		if(!ActiveMouse)
			m_isMoving = -1;
	}

	// Use this for initialization
	void Start () {
		m_basePos = this.transform.position;

		m_originalColor = this.GetComponent<Text>().color;

		if(ActiveMouse)
		{
			EventTrigger.Entry pointerEnterEvent = new EventTrigger.Entry();
			pointerEnterEvent.eventID = EventTriggerType.PointerEnter;
			pointerEnterEvent.callback.AddListener((eventData) => { OnPointerEnter(); });

			EventTrigger.Entry pointerExitEvent = new EventTrigger.Entry();
			pointerExitEvent.eventID = EventTriggerType.PointerExit;
			pointerExitEvent.callback.AddListener((eventData) => { OnPointerExit(); });

			this.gameObject.AddComponent<EventTrigger>();
			this.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerEnterEvent);
			this.gameObject.GetComponent<EventTrigger>().triggers.Add(pointerExitEvent);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_hoverState != m_oldHoverState)
		{
			if (m_hoverState)
				m_isMoving = 1;
			else
				m_isMoving = -1;
			m_oldHoverState = m_hoverState;
		}

		if (m_isMoving == 1)
		{
			if (Vector3.Distance(m_basePos, this.transform.position) > Distance)
			{
				this.GetComponent<Text>().color = SelectColor;
				m_isMoving = 0;
			}
			else
			{
				this.transform.Translate(Vector3.back * Speed * Time.deltaTime);
			}
		}
		else if(m_isMoving == -1)
		{
			if (Vector3.Distance(m_basePos, this.transform.position) < 0.1)
			{
				this.GetComponent<Text>().color = m_originalColor;
				this.transform.position = m_basePos;
				m_isMoving = 0;
			}
			else
				this.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}
	}
}
