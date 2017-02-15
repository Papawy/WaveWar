using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

	public Transform m_target;
	public float m_distance = 5.0f;
	public float m_xSpeed = 120.0f;
	public float m_ySpeed = 120.0f;

	public float m_yMinLimit = -20f;
	public float m_yMaxLimit = 80f;

	public float m_distanceMin = .5f;
	public float m_distanceMax = 15f;


	private Rigidbody m_rigidbody = null;

	float x = 0.0f;
	float y = 0.0f;

	void Update()
	{

	}

	// Use this for initialization
	void Start()
	{

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		m_rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (m_rigidbody != null)
		{
			m_rigidbody.freezeRotation = true;
		}
	}

	void LateUpdate()
	{
		if (m_target)
		{
			x += TeamUtility.IO.InputManager.GetAxis("LookHorizontal") * m_xSpeed * m_distance * 0.02f;
			y -= TeamUtility.IO.InputManager.GetAxis("LookVertical") * m_ySpeed * 0.02f;

			y = ClampAngle(y, m_yMinLimit, m_yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			m_distance = Mathf.Clamp(m_distance - TeamUtility.IO.InputManager.GetAxis("MouseWheel") * 5, m_distanceMin, m_distanceMax);

			RaycastHit hit;
			
			if (Physics.Linecast(m_target.position, transform.position, out hit))
			{
				m_distance -= hit.distance;
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_distance);
			Vector3 tarPos = m_target.position + Vector3.up;
			Vector3 position = rotation * negDistance + tarPos + this.transform.right;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
