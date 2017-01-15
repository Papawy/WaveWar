using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelect : MonoBehaviour {

	public AudioClip selectSound;
	public float Volume = 1.0f;

	private AudioSource source;

	void OnMenuSelect(Text text)
	{
		source.PlayOneShot(selectSound, Volume);
	}

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
