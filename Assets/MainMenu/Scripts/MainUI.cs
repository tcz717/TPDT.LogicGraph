using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void StartGame()
	{
		Application.LoadLevel ("Game");
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
