using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	GameUI game;
	// Use this for initialization
	void Start () 
	{
        game = GameUI.MainUI; ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp(0)) 
		{
			game.OnSelect(gameObject);
		} 
	}
}
