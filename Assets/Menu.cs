using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public int btnWidth;
	public int btnHeight;
	public GUISkin skinButton;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.skin = skinButton;
		if (GUI.Button (new Rect (((Screen.width+btnWidth)/2)-btnWidth,((Screen.height+btnHeight)/2)-btnHeight,btnWidth,btnHeight), "New Game")) {
			Application.LoadLevel("Game");
		}
	}
}
