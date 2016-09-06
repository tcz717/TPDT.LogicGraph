using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 150, Screen.height / 4.0f, 300, Screen.height / 2.0f));
		{
			GUILayout.BeginVertical ();
			{
				GUILayout.BeginHorizontal ();
				{
					GUILayout.FlexibleSpace ();
					GUILayout.Label ("编程棋");
					GUILayout.FlexibleSpace ();
				}
				GUILayout.EndHorizontal ();
				
				GUILayout.BeginHorizontal ();
				{
					GUILayout.FlexibleSpace ();
					GUILayout.Label ("测试版");
					GUILayout.Space (100);
				}
				GUILayout.EndHorizontal ();

				GUILayout.FlexibleSpace ();
				
				if(GUILayout.Button ("开始游戏"))
				{
					Application.LoadLevel("Game");
				}
				if(GUILayout.Button ("退出"))
				{
					Application.Quit();
				}
			}
			GUILayout.EndVertical ();
		}
		GUILayout.EndArea ();
	}
}
